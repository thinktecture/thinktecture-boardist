using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.DTOs;
using Thinktecture.Boardist.WebApi.Extensions;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class PublishersService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;
    private readonly FilesService _filesService;

    public PublishersService(IMapper mapper, BoardistContext boardistContext, FilesService filesService)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
      _filesService = filesService;
    }

    public async Task<PublisherDto[]> GetAllAsync()
    {
      return await _mapper.ProjectTo<PublisherDto>(_boardistContext.Publishers.WithoutDeleted().OrderBy(p => p.Name)).ToArrayAsync();
    }

    public async Task<PublisherDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<PublisherDto>(_boardistContext.Publishers.WithoutDeleted().Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<PublisherDto> CreateAsync(PublisherDto publisher)
    {
      var dbPublisher = _mapper.Map<Publisher>(publisher);

      await _boardistContext.Publishers.AddAsync(dbPublisher);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Publisher, PublisherDto>(dbPublisher);
    }

    public async Task DeleteAsync(Guid id)
    {
      var dbPublisher = new Publisher() { Id = id, IsDeleted = true };

      _boardistContext.Attach(dbPublisher);
      await _boardistContext.SaveChangesAsync();
      _filesService.Delete(id);
    }

    public async Task<PublisherDto> UpdateAsync(PublisherDto publisher)
    {
      var dbPublisher = new Publisher() { Id = publisher.Id };

      _boardistContext.Attach(dbPublisher);

      _mapper.Map(publisher, dbPublisher);

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Publisher, PublisherDto>(dbPublisher);
    }
  }
}
