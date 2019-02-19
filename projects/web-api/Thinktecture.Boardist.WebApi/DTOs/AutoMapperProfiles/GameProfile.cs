using System.Linq;
using AutoMapper;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.DTOs.AutoMapperProfiles
{
  public class GameProfile : Profile
  {
    public GameProfile()
    {
      CreateMap<Game, GameDto>()
        .ForMember(p => p.Authors, opt => opt.MapFrom(m => m.Authors.Select(a => a.AuthorId)))
        .ForMember(p => p.Illustrators, opt => opt.MapFrom(m => m.Illustrators.Select(a => a.IllustratorId)))
        .ForMember(p => p.Categories, opt => opt.MapFrom(m => m.Categories.Select(a => a.CategoryId)));
    }
  }
}
