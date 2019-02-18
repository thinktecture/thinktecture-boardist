using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
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
  }
}
