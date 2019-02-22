using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Extensions;
using Thinktecture.Boardist.WebApi.Models;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class GamesService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;
    private readonly FilesService _filesService;

    public GamesService(IMapper mapper, BoardistContext boardistContext, FilesService filesService)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
      _filesService = filesService;
    }

    public async Task<GameDto[]> GetAllAsync(bool includeExpansions)
    {
      var query = _boardistContext.Games.AsQueryable();

      if (!includeExpansions)
      {
        query = query.Where(p => p.MainGame == null);
      }

      var games = await _mapper.ProjectTo<GameDto>(query.WithoutDeleted().OrderBy(p => p.Name)).ToArrayAsync();

      foreach (var game in games)
      {
        game.HasRules = _filesService.Exists(game.Id, FileCategory.Rules);
      }

      return games;
    }

    public async Task<GameDto> GetAsync(Guid id)
    {
      var game = await _mapper.ProjectTo<GameDto>(_boardistContext.Games.WithoutDeleted().Where(p => p.Id == id)).SingleOrDefaultAsync();

      game.HasRules = _filesService.Exists(game.Id, FileCategory.Rules);

      return game;
    }

    public async Task<GameDto> CreateAsync(GameDto game)
    {
      var dbGame = _mapper.Map<GameDto, Game>(game);
      dbGame.Id = Guid.NewGuid();

      await _boardistContext.Games.AddAsync(dbGame);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Game, GameDto>(dbGame);
    }

    public async Task DeleteAsync(Guid id)
    {
      var dbGame = new Game() { Id = id, IsDeleted = true };

      _boardistContext.Attach(dbGame);

      await _boardistContext.SaveChangesAsync();
      _filesService.Delete(id);
    }

    public async Task<GameDto> UpdateAsync(GameDto game)
    {
      var dbGame = await _boardistContext.Games
        .Include(p => p.Authors)
        .Include(p => p.Illustrators)
        .Include(p => p.Categories)
        .Include(p => p.Mechanics)
        .SingleOrDefaultAsync(g => g.Id == game.Id);

      dbGame.Authors.Clear();
      dbGame.Illustrators.Clear();
      dbGame.Categories.Clear();
      dbGame.Mechanics.Clear();

      _mapper.Map(game, dbGame);

      _boardistContext.Entry(dbGame).Property(p => p.BoardGameGeekId).IsModified = true;

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Game, GameDto>(dbGame);
    }
  }
}
