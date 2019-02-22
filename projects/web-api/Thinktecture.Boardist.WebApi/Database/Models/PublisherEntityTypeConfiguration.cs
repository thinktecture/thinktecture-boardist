using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class PublisherEntityTypeConfiguration : SyncableEntityTypeConfiguration<Publisher>
  {
    public override void Configure(EntityTypeBuilder<Publisher> builder)
    {
      base.Configure(builder);
      
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
