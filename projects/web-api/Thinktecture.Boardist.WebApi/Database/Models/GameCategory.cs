using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameCategory
  {
    public Game Game { get; set; }
    public Category Category { get; set; }

    public Guid GameId { get; set; }
    public Guid CategoryId { get; set; }
  }
}
