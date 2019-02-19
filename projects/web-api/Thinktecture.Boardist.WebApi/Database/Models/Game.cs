using System;
using System.Collections.Generic;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Game
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int MinAge { get; set; }
    public int? MinDuration { get; set; }
    public int? MaxDuration { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    public int? BoardGameGeekId { get; set; }

    public Guid? MainGameId { get; set; }
    public Game MainGame { get; set; }
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; }

    public ICollection<GameAuthor> Authors { get; set; }
    public ICollection<GameIllustrator> Illustrators { get; set; }
    public ICollection<GameCategory> Categories { get; set; }
    public ICollection<GameMechanic> Mechanics { get; set; }
  }
}
