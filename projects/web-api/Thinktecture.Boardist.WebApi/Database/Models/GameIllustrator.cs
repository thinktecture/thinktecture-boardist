using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameIllustrator
  {
    public Game Game { get; set; }
    public Person Illustrator { get; set; }

    public Guid GameId { get; set; }
    public Guid IllustratorId { get; set; }
  }
}
