using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
  {
    public void Configure(EntityTypeBuilder<Person> builder)
    {
      builder.HasKey(p => p.Id);
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
