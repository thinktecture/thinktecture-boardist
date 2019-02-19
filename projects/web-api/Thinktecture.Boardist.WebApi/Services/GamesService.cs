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

    public GamesService(IMapper mapper, BoardistContext boardistContext)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
    }

    public async Task<GameDto[]> GetAllAsync()
    {
      return await _mapper.ProjectTo<GameDto>(_boardistContext.Games).ToArrayAsync();
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

        return true;
      }
      catch (DbUpdateConcurrencyException)
      {
        return false;
      }
    }
  }
}
