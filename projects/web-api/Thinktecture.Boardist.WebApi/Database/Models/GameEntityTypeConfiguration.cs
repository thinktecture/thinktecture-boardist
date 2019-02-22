using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
  {
    public void Configure(EntityTypeBuilder<Game> builder)
    {
      builder.HasKey(p => p.Id);
      builder.Property(p => p.Name).HasMaxLength(BoardistContext.MaxStringLength);

      builder
        .HasOne(p => p.Publisher)
        .WithMany()
        .HasForeignKey(p => p.PublisherId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);

      builder
        .HasOne(p => p.MainGame)
        .WithMany()
        .HasForeignKey(p => p.MainGameId)
        .IsRequired(false);

      builder.HasMany(p => p.Categories).WithOne();

      builder.Property(p => p.BuyPrice).HasColumnType("decimal(3,2)");
    }
  }
}
