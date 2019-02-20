using AutoMapper;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.DTOs.AutoMapperProfiles
{
  public class MechanicProfile : Profile
  {
    public MechanicProfile()
    {
      CreateMap<Mechanic, MechanicDto>();
      CreateMap<MechanicDto, Mechanic>();
    }
  }
}
