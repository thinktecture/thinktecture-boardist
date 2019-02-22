using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class PersonEntityTypeConfiguration : SyncableEntityTypeConfiguration<Person>
  {
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
      base.Configure(builder);
      
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);
    }
  }
}
