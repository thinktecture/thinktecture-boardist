using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public interface IGameRelation
  {
    Guid DestinationId { get; set; }
  }
}