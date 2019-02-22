using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public abstract class SyncableEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class, ISyncable
  {
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
      builder.HasKey(p => p.Id);
      builder.Property(p => p.RowVersion).IsRowVersion();
    }
  }
}
