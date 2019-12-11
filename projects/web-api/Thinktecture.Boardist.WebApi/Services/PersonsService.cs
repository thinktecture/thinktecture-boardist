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
      return await _mapper.ProjectTo<PersonDto>(_boardistContext.Persons.WithoutDeleted().OrderBy(p => p.Name)).ToArrayAsync();
    }

    public async Task<PersonDto> GetAsync(Guid id)
    {
      return await _mapper.ProjectTo<PersonDto>(_boardistContext.Persons.WithoutDeleted().Where(p => p.Id == id)).SingleOrDefaultAsync();
    }

    public async Task<PersonDto> CreateAsync(PersonDto person)
    {
      var dbPerson = _mapper.Map<Person>(person);

      await _boardistContext.Persons.AddAsync(dbPerson);
      await _boardistContext.SaveChangesAsync();

      return _mapper.Map<Person, PersonDto>(dbPerson);
    }

    public async Task DeleteAsync(Guid id)
    {
      var dbPerson = await _boardistContext.Persons.SingleAsync(p => p.Id == id);

      dbPerson.IsDeleted = true;

      await _boardistContext.SaveChangesAsync();
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
