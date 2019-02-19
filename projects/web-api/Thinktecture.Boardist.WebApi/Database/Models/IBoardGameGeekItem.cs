using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public interface IBoardGameGeekItem
  {
    Guid Id { get; set; }
    string Name { get; set; }
    int? BoardGameGeekId { get; set; }
  }
}
