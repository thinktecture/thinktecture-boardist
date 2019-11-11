using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Syncable : ISyncable
  {
    public Guid Id { get; set; }
    public ulong RowVersion { get; set; }
    public bool IsDeleted { get; set; }
  }
}
