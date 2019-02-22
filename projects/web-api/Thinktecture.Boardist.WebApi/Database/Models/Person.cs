using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Person : Syncable, IBoardGameGeekItem
  {
    public string Name { get; set; }

    public int? BoardGameGeekId { get; set; }
  }
}
