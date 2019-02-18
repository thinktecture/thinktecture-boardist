using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.DTOs;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class PublisherService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;

    public PublisherService(IMapper mapper, BoardistContext boardistContext)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
    }

    public async Task<PublisherDto[]> GetAll()
    {
      return await _mapper.ProjectTo<PublisherDto>(_boardistContext.Publishers).ToArrayAsync();
    }

    public async Task<PublisherDto> Get(Guid id)
    {
      return await _mapper.ProjectTo<PublisherDto>(_boardistContext.Publishers.Where(p => p.Id == id)).SingleOrDefaultAsync();
    }
  }
}
