using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameMechanic : IGameRelation
  {
    public Game Game { get; set; }
    public Mechanic Mechanic { get; set; }

    public Guid GameId { get; set; }
    public Guid MechanicId { get; set; }
    
    Guid IGameRelation.DestinationId { get => MechanicId; set => MechanicId = value; }
  }
}
