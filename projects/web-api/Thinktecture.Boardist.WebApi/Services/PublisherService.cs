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
  public class PublisherService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;

    public PublisherService(IMapper mapper, BoardistContext boardistContext)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
    }

    public async Task<PublisherDto[]> GetAllAsync()
    {
      return await _mapper.ProjectTo<PublisherDto>(_boardistContext.Publishers).ToArrayAsync();
    }

    public async Task<PublisherDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<PublisherDto>(_boardistContext.Publishers.Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<PublisherDto> CreateAsync(PublisherDto publisher)
    {
      var dbPublisher = new Publisher() {Name = publisher.Name, Id = Guid.NewGuid()};

      await _boardistContext.Publishers.AddAsync(dbPublisher);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Publisher, PublisherDto>(dbPublisher);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var dbPublisher = new Publisher() {Id = id};

      _boardistContext.Entry(dbPublisher).State = EntityState.Deleted;

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

    public async Task<PublisherDto> UpdateAsync(PublisherDto publisher)
    {
      var dbPublisher = new Publisher() {Id = publisher.Id};

      _boardistContext.Attach(dbPublisher);

      dbPublisher.Name = publisher.Name;

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Publisher, PublisherDto>(dbPublisher);
    }
  }
}
