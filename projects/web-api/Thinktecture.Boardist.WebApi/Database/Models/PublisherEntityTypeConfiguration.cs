using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
  {
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
      builder.HasKey(p => p.Id);
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
