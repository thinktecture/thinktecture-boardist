using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.HasKey(p => p.Id);
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
