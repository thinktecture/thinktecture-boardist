using System;
using System.Collections.Generic;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public class GameDto : ItemDto
  {
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int MinAge { get; set; }
    public int? MinDuration { get; set; }
    public int? MaxDuration { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }

    public Guid? PublisherId { get; set; }
    public Guid? MainGameId { get; set; }

    public ICollection<Guid> Authors { get; set; }
    public ICollection<Guid> Illustrators { get; set; }
    public ICollection<Guid> Categories { get; set; }
    public ICollection<Guid> Mechanics { get; set; }

    public bool HasRules { get; set; }
  }
}
