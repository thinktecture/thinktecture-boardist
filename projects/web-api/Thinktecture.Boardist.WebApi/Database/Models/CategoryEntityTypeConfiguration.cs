using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class CategoryEntityTypeConfiguration : SyncableEntityTypeConfiguration<Category>
  {
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
      base.Configure(builder);
      
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
