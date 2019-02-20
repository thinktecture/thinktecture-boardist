using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;

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
      
      return await _mapper.ProjectTo<GameDto>(query).ToArrayAsync();
    }

    public async Task<GameDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<GameDto>(_boardistContext.Games.Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<GameDto> CreateAsync(GameDto game)
    {
      var dbGame = _mapper.Map<GameDto, Game>(game);
      dbGame.Id = Guid.NewGuid();

      await _boardistContext.Games.AddAsync(dbGame);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Game, GameDto>(dbGame);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
      var dbGame = new Game() {Id = id};

      _boardistContext.Entry(dbGame).State = EntityState.Deleted;

      try
      {
        await _boardistContext.SaveChangesAsync();
        _filesService.Delete(id);

        return true;
      }
      catch (DbUpdateConcurrencyException)
      {
        return false;
      }
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
      
      _mapper.Map(game, dbGame, typeof(GameDto), typeof(Game));

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Game, GameDto>(dbGame);
    }
  }
}
