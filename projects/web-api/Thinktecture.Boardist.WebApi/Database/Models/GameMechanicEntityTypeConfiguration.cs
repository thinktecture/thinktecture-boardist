using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameMechanicEntityTypeConfiguration : IEntityTypeConfiguration<GameMechanic>
  {
    public void Configure(EntityTypeBuilder<GameMechanic> builder)
    {
      builder.HasKey(p => new {p.GameId, p.MechanicId});

      builder
        .HasOne(p => p.Game)
        .WithMany(p => p.Mechanics)
        .HasForeignKey(p => p.GameId);

      builder
        .HasOne(p => p.Mechanic)
        .WithMany()
        .HasForeignKey(p => p.MechanicId);
    }
  }
}
