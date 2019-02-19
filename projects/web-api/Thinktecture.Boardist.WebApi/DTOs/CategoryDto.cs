using System;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public class CategoryDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? BoardGameGeekId { get; set; }
  }
}
