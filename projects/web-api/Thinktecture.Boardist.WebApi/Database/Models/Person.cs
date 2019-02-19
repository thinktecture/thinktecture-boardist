using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Person : IBoardGameGeekItem
  {
    public Guid Id { get; set; }
    public string Name { get; set; }

    public int? BoardGameGeekId { get; set; }
  }
}
