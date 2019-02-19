using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameIllustrator : IGameRelation
  {
    public Game Game { get; set; }
    public Person Illustrator { get; set; }

    public Guid GameId { get; set; }
    public Guid IllustratorId { get; set; }
    
    Guid IGameRelation.DestinationId { get => IllustratorId; set => IllustratorId = value; }
  }
}
