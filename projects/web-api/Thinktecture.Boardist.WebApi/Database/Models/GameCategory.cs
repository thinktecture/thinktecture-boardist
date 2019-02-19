using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameCategory : IGameRelation
  {
    public Game Game { get; set; }
    public Category Category { get; set; }

    public Guid GameId { get; set; }
    public Guid CategoryId { get; set; }
    
    Guid IGameRelation.DestinationId { get => CategoryId; set => CategoryId = value; }
  }
}
