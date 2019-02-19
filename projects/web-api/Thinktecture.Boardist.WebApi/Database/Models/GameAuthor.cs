using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameAuthor : IGameRelation
  {
    public Game Game { get; set; }
    public Person Author { get; set; }

    public Guid GameId { get; set; }
    public Guid AuthorId { get; set; }
    
    Guid IGameRelation.DestinationId { get => AuthorId; set => AuthorId = value; }
  }
}
