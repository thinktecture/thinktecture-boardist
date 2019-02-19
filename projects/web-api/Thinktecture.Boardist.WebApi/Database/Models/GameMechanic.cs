using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameMechanic
  {
    public Game Game { get; set; }
    public Mechanic Mechanic { get; set; }

    public Guid GameId { get; set; }
    public Guid MechanicId { get; set; }
  }
}
