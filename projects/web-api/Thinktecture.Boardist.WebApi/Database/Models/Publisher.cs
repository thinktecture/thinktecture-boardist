using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Publisher : IBoardGameGeekItem
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }

    public int? BoardGameGeekId { get; set; }
  }
}
