using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public interface ISyncable
  {
    Guid Id { get; set; }
    byte[] RowVersion { get; set; }
    bool IsDeleted { get; set; }
  }
}