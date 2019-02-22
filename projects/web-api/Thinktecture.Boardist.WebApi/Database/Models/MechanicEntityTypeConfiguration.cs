using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class MechanicEntityTypeConfiguration : IEntityTypeConfiguration<Mechanic>
  {
    public void Configure(EntityTypeBuilder<Mechanic> builder)
    {
      builder.HasKey(p => p.Id);
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
