using System;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public abstract class SyncableDto
  {
    public Guid Id { get; set; } = Guid.NewGuid();
  }
}
