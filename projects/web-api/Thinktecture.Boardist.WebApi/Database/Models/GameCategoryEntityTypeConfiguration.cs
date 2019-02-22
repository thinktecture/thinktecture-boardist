using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameCategoryEntityTypeConfiguration : IEntityTypeConfiguration<GameCategory>
  {
    public void Configure(EntityTypeBuilder<GameCategory> builder)
    {
      builder.HasKey(p => new {p.GameId, p.CategoryId});

      builder
        .HasOne(p => p.Game)
        .WithMany(p => p.Categories)
        .HasForeignKey(p => p.GameId);

      builder
        .HasOne(p => p.Category)
        .WithMany()
        .HasForeignKey(p => p.CategoryId);
    }
  }
}
