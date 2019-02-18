using AutoMapper;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.DTOs.AutoMapperProfiles
{
  public class GameProfile : Profile
  {
    public GameProfile()
    {
      CreateMap<Game, GameDto>();
    }
  }
}
