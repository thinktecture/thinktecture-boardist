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
  public class PersonsService
  {
    private readonly IMapper _mapper;
    private readonly BoardistContext _boardistContext;

    public PersonsService(IMapper mapper, BoardistContext boardistContext)
    {
      _mapper = mapper;
      _boardistContext = boardistContext;
    }

    public async Task<PersonDto[]> GetAllAsync()
    {
      return await _mapper.ProjectTo<PersonDto>(_boardistContext.Persons).ToArrayAsync();
    }

    public async Task<PersonDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<PersonDto>(_boardistContext.Persons.Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<PersonDto> CreateAsync(PersonDto person)
    {
      var dbPerson = _mapper.Map<Person>(person);

      await _boardistContext.Persons.AddAsync(dbPerson);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Person, PersonDto>(dbPerson);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var dbPerson = new Person() { Id = id };

      _boardistContext.Entry(dbPerson).State = EntityState.Deleted;

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

    public async Task<PersonDto> UpdateAsync(PersonDto person)
    {
      var dbPerson = new Person() { Id = person.Id };

      _boardistContext.Attach(dbPerson);

      _mapper.Map(person, dbPerson);

      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Person, PersonDto>(dbPerson);
    }
  }
}
