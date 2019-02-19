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
        .ForMember(p => p.Categories, opt => opt.MapFrom(m => m.Categories.Select(a => a.CategoryId)))
        .ForMember(p => p.Mechanics, opt => opt.MapFrom(m => m.Mechanics.Select(a => a.MechanicId)));

      CreateMap<GameDto, Game>()
        .ForMember(p => p.Authors, opt => opt.MapFrom(m => m.Authors.Select(s => new GameAuthor() {AuthorId = s})))
        .ForMember(p => p.Illustrators, opt => opt.MapFrom(m => m.Illustrators.Select(s => new GameIllustrator() {IllustratorId = s})))
        .ForMember(p => p.Categories, opt => opt.MapFrom(m => m.Categories.Select(s => new GameCategory() {CategoryId = s})))
        .ForMember(p => p.Mechanics, opt => opt.MapFrom(m => m.Mechanics.Select(s => new GameMechanic() {MechanicId = s})));
    }
  }
}
