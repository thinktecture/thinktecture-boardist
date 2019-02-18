using AutoMapper;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.DTOs.AutoMapperProfiles
{
  public class PublisherProfile : Profile
  {
    public PublisherProfile()
    {
      CreateMap<Publisher, PublisherDto>();
    }
  }
}
