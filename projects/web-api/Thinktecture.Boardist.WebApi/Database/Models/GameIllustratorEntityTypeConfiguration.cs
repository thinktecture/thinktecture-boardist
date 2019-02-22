using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameIllustratorEntityTypeConfiguration : IEntityTypeConfiguration<GameIllustrator>
  {
    public void Configure(EntityTypeBuilder<GameIllustrator> builder)
    {
      builder.HasKey(p => new {p.GameId, p.IllustratorId});

      builder
        .HasOne(p => p.Game)
        .WithMany(p => p.Illustrators)
        .HasForeignKey(p => p.GameId);

      builder
        .HasOne(p => p.Illustrator)
        .WithMany()
        .HasForeignKey(p => p.IllustratorId);
    }
  }
}
