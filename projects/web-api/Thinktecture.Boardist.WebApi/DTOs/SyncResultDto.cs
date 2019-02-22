using System;
using System.Collections.Generic;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  public class SyncDto<T>
    where T: SyncableDto
  {
    public byte[] Timestamp { get; set; }
    public ICollection<T> Changed { get; set; }
    public ICollection<Guid> Deleted { get; set; }
  }
}
