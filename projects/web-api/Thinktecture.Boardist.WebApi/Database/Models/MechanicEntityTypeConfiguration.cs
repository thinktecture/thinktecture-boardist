using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class MechanicEntityTypeConfiguration : SyncableEntityTypeConfiguration<Mechanic>
  {
    public override void Configure(EntityTypeBuilder<Mechanic> builder)
    {
      base.Configure(builder);
      
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
