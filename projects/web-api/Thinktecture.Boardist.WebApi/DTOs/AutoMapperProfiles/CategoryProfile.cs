using AutoMapper;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.DTOs.AutoMapperProfiles
{
  public class CategoryProfile : Profile
  {
    public CategoryProfile()
    {
      CreateMap<Category, CategoryDto>();
    }
  }
}
