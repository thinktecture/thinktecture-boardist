using System;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public abstract class ItemDto
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int? BoardGameGeekId { get; set; }
  }
}
