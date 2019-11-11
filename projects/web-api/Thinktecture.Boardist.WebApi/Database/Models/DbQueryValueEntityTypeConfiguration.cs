using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class DbQueryValueEntityTypeConfiguration : IEntityTypeConfiguration<DbQueryValue>
  {
    public void Configure(EntityTypeBuilder<DbQueryValue> builder)
    {
      builder.ToView(nameof(DbQueryValue)).HasNoKey();
      builder.Property(p => p.Value)
        .HasColumnType("rowversion")
        .IsRowVersion()
        .HasConversion(Converters.RowVersionConverter());
    }
  }
}
