using System;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public class GameDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int? MinDuration { get; set; }
    public int? MaxDuration { get; set; }
    public int? PerPlayerDuration { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    
    public Guid PublisherId { get; set; }
    public Guid? MainGameId { get; set; }
  }
}
