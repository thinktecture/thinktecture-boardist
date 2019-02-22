using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Publisher : Syncable, IBoardGameGeekItem
  {
    public string Name { get; set; }
    public int Priority { get; set; }

    public int? BoardGameGeekId { get; set; }
  }
}
