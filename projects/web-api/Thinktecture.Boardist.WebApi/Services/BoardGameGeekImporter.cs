using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Models;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class BoardGameGeekImporter
  {
    private const string BoardGameGeekApiUrl = "https://www.boardgamegeek.com/xmlapi2";
    private const string BoardGameType = "boardgame";
    private const string BoardGamePublisherType = "boardgamepublisher";
    private const string BoardGameDesignerType = "boardgamedesigner";
    private const string BoardGameArtistType = "boardgameartist";
    private const string BoardGameCategoryType = "boardgamecategory";
    private const string BoardGameMechanicType = "boardgamemechanic";
    private const string BoardGameExpansionType = "boardgameexpansion";

    private readonly BoardistContext _boardistContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FilesService _filesService;
    private readonly IMapper _mapper;

    public BoardGameGeekImporter(BoardistContext boardistContext,
      IHttpClientFactory httpClientFactory,
      FilesService filesService,
      IMapper mapper)
    {
      _boardistContext = boardistContext;
      _httpClientFactory = httpClientFactory;
      _filesService = filesService;
      _mapper = mapper;
    }

    public async Task<GameDto> ImportAsync(Guid id, bool overwrite)
    {
      using (var transaction = await _boardistContext.Database.BeginTransactionAsync())
      {
        var dbGame = await _boardistContext.Games
          .Include(p => p.Categories)
          .Include(p => p.Authors)
          .Include(p => p.Illustrators)
          .Include(p => p.Mechanics)
          .SingleOrDefaultAsync(p => p.Id == id);

        if (dbGame?.BoardGameGeekId == null)
        {
          return null;
        }

        var boardGameGeekResult = await QueryBoardGameGeekApiAsync(dbGame.BoardGameGeekId.Value);

        if (boardGameGeekResult == null)
        {
          return null;
        }

        dbGame.IsDeleted = false;

        ImportSimpleValues(boardGameGeekResult, dbGame, overwrite);

        await ImportPublisherRelationAsync(boardGameGeekResult, dbGame, overwrite);

        await ImportBoardGameGeekRelationsAsync<Category, GameCategory>(boardGameGeekResult, BoardGameCategoryType, dbGame.Categories, overwrite);
        await ImportBoardGameGeekRelationsAsync<Mechanic, GameMechanic>(boardGameGeekResult, BoardGameMechanicType, dbGame.Mechanics, overwrite);
        await ImportBoardGameGeekRelationsAsync<Person, GameAuthor>(boardGameGeekResult, BoardGameDesignerType, dbGame.Authors, overwrite);
        await ImportBoardGameGeekRelationsAsync<Person, GameIllustrator>(boardGameGeekResult, BoardGameArtistType, dbGame.Illustrators, overwrite);

        await ImportMainGameRelationAsync(boardGameGeekResult, dbGame, overwrite);

        await _boardistContext.SaveChangesAsync();

        transaction.Commit();

        return _mapper.Map<Game, GameDto>(dbGame);
      }
    }

    private async Task ImportPublisherRelationAsync(BoardGameGeekApiResult.BoardGame boardGameGeekResult, Game dbGame, bool overwrite)
    {
      var xmlPublishers = boardGameGeekResult.Link.Where(p => p.Type == BoardGamePublisherType).ToArray();

      if (xmlPublishers.Length > 0 && (overwrite || !dbGame.PublisherId.HasValue))
      {
        var xmlPublisherIds = xmlPublishers.Select(p => p.Id).ToArray();
        var xmlPublisherNames = xmlPublishers.Select(p => p.Value).ToArray();

        var dbPublisher = await _boardistContext.Publishers
          .Where(p => xmlPublisherIds.Contains(p.BoardGameGeekId.GetValueOrDefault()))
          .OrderByDescending(p => p.Priority).ThenBy(p => p.Name)
          .FirstOrDefaultAsync();

        if (dbPublisher == null)
        {
          dbPublisher = await _boardistContext.Publishers
            .Where(p => xmlPublisherNames.Contains(p.Name))
            .OrderByDescending(p => p.Priority).ThenBy(p => p.Name)
            .FirstOrDefaultAsync();

          if (dbPublisher != null)
          {
            dbPublisher.BoardGameGeekId =
              xmlPublishers.Single(p => p.Value.Equals(dbPublisher.Name, StringComparison.OrdinalIgnoreCase)).Id;
          }
        }

        if (dbPublisher == null)
        {
          var xmlPublisher = xmlPublishers.First();

          dbPublisher = new Publisher() { Name = xmlPublisher.Value, Id = Guid.NewGuid(), BoardGameGeekId = xmlPublisher.Id };
          _boardistContext.Publishers.Add(dbPublisher);
        }

        dbPublisher.IsDeleted = false;

        dbGame.PublisherId = dbPublisher.Id;
      }
    }

    private async Task ImportMainGameRelationAsync(BoardGameGeekApiResult.BoardGame boardGameGeekResult, Game dbGame, bool overwrite)
    {
      var xmlInbounds = boardGameGeekResult.Link.Where(p => p.Inbound && p.Type == BoardGameExpansionType).Select(p => p.Id).ToArray();

      if (xmlInbounds.Length > 0 && (overwrite || !dbGame.MainGameId.HasValue))
      {
        var dbMainGame = await _boardistContext.Games
          .Where(p => p.MainGameId == null)
          .FirstOrDefaultAsync(p => xmlInbounds.Contains(p.BoardGameGeekId.GetValueOrDefault()));

        if (dbMainGame == null)
        {
          return;
        }

        dbGame.MainGameId = dbMainGame.Id;
        dbGame.IsDeleted = false;

        await _boardistContext.SaveChangesAsync();
      }
    }

    private async Task ImportBoardGameGeekRelationsAsync<TSource, TResult>(BoardGameGeekApiResult.BoardGame boardGameGeekResult,
      string linkType, ICollection<TResult> destination, bool overwrite)
      where TSource : class, IBoardGameGeekItem, new()
      where TResult : class, IGameRelation, new()
    {
      if (overwrite || destination.Count == 0)
      {
        var links = boardGameGeekResult.Link.Where(p => p.Type == linkType).ToList();

        if (links.Count == 0)
        {
          return;
        }

        destination.Clear();

        var source = _boardistContext.Set<TSource>();

        var dbItems = source.ToLookup(p => p.BoardGameGeekId);

        foreach (var link in links.Where(p => !dbItems.Contains(p.Id)))
        {
          var item = dbItems[null].FirstOrDefault(p => p.Name == link.Value);

          if (item != null)
          {
            item.BoardGameGeekId = link.Id;
          }
        }

        var itemsToAdd = links
          .Select(p => p.Id)
          .Except(dbItems
            .SelectMany(p => p)
            .Where(p => p.BoardGameGeekId.HasValue)
            .Select(p => p.BoardGameGeekId.Value)
          )
          .Select(id => links.Single(x => x.Id == id))
          .ToList();

        itemsToAdd
          .Select(p =>
          {
            var item = new TSource()
            {
              Id = Guid.NewGuid(),
              Name = p.Value,
              BoardGameGeekId = p.Id
            };
            source.Add(item);
            return item;
          })
          .Select(p => p.Id)
          .Concat(links
            .Except(itemsToAdd)
            .Select(p => dbItems
              .SelectMany(x => x)
              .Single(x => x.BoardGameGeekId == p.Id)
            )
            .Select(p => p.Id)
          )
          .Select(p => new TResult() { DestinationId = p })
          .ToList()
          .ForEach(destination.Add);

        await _boardistContext.SaveChangesAsync();
      }
    }

    public async Task ImportImageAsync(Guid gameId)
    {
      var dbGame = await _boardistContext.Games.SingleOrDefaultAsync(p => p.Id == gameId);

      if (dbGame?.BoardGameGeekId == null)
      {
        return;
      }

      var boardGameGeekResult = await QueryBoardGameGeekApiAsync(dbGame.BoardGameGeekId.Value);

      if (boardGameGeekResult == null)
      {
        return;
      }

      await DownloadImageAsync(boardGameGeekResult, gameId);
    }

    private async Task DownloadImageAsync(BoardGameGeekApiResult.BoardGame boardGameGeekResult, Guid gameId)
    {
      if (string.IsNullOrWhiteSpace(boardGameGeekResult.Image))
      {
        return;
      }

      var httpClient = _httpClientFactory.CreateClient();

      var responseMessage = await httpClient.GetAsync(boardGameGeekResult.Image);
      var stream = await responseMessage.Content.ReadAsStreamAsync();
      await _filesService.SaveAsync(stream, gameId, FileCategory.Logo, responseMessage.Content.Headers.ContentType);
    }

    private void ImportSimpleValues(BoardGameGeekApiResult.BoardGame boardGameGeekResult, Game dbGame, bool overwrite)
    {
      if (boardGameGeekResult.MinAge != 0 && (overwrite || dbGame.MinAge == 0))
      {
        dbGame.MinAge = boardGameGeekResult.MinAge;
      }

      if (boardGameGeekResult.MaxPlayers != 0 && (overwrite || dbGame.MaxPlayers == 0))
      {
        dbGame.MaxPlayers = boardGameGeekResult.MaxPlayers;
      }

      if (boardGameGeekResult.MinPlayers != 0 && (overwrite || dbGame.MinPlayers == 0))
      {
        dbGame.MinPlayers = boardGameGeekResult.MinPlayers;
      }

      if (boardGameGeekResult.MinPlayTime != 0 && (overwrite || !dbGame.MinDuration.HasValue))
      {
        dbGame.MinDuration = (int)Math.Ceiling(boardGameGeekResult.MinPlayTime / 5m) * 5;
      }

      if (boardGameGeekResult.MaxPlayTime != 0 && (overwrite || !dbGame.MaxDuration.HasValue))
      {
        dbGame.MaxDuration = (int)Math.Ceiling(boardGameGeekResult.MaxPlayTime / 5m) * 5;
      }
    }

    private async Task<BoardGameGeekApiResult.BoardGame> QueryBoardGameGeekApiAsync(int boardGameGeekId)
    {
      var httpClient = _httpClientFactory.CreateClient();

      var httpResult = await httpClient.GetByteArrayAsync($"{BoardGameGeekApiUrl}/thing?id={boardGameGeekId}");
      using (var memoryStream = new MemoryStream(httpResult))
      {
        using (var xmlTextReader = new XmlTextReader(memoryStream))
        {
          var xmlSerializer = new XmlSerializer(typeof(BoardGameGeekApiResult));
          var result = (BoardGameGeekApiResult)xmlSerializer.Deserialize(xmlTextReader);
          return result.Game;
        }
      }
    }

    private async Task<BoardGameGeekApiResult.BoardGame> QueryBoardGameGeekApiAsync(string query)
    {
      var httpClient = _httpClientFactory.CreateClient();

      var httpResult = await httpClient.GetByteArrayAsync(
        $"{BoardGameGeekApiUrl}/search?query={UrlEncoder.Default.Encode(query)}&exact=1&type={BoardGameType},{BoardGameExpansionType}");
      using (var memoryStream = new MemoryStream(httpResult))
      {
        using (var xmlTextReader = new XmlTextReader(memoryStream))
        {
          var xmlSerializer = new XmlSerializer(typeof(BoardGameGeekApiResult));
          var result = (BoardGameGeekApiResult)xmlSerializer.Deserialize(xmlTextReader);
          return result.Game;
        }
      }
    }

    public async Task<int?> LookupAsync(string query)
    {
      if (query == "ALL")
      {
        var games = await _boardistContext.Games.Where(g => g.BoardGameGeekId == null).ToListAsync();

        try
        {
          foreach (var game in games)
          {
            Thread.Sleep(5000);
            var result = await QueryBoardGameGeekApiAsync(game.Name);
            if (result != null)
            {
              game.BoardGameGeekId = result.Id;
            }

            await _boardistContext.SaveChangesAsync();
          }
        }
        catch
        {
        }

        return null;
      }

      var boardGameGeekResult = await QueryBoardGameGeekApiAsync(query);

      return boardGameGeekResult?.Id;
    }
  }
}
