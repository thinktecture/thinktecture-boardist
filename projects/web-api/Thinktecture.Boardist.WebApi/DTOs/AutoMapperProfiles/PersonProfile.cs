using AutoMapper;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.DTOs.AutoMapperProfiles
{
  public class PersonProfile : Profile
  {
    public PersonProfile()
    {
      CreateMap<Person, PersonDto>();
      CreateMap<PersonDto, Person>();
    }
  }
}
