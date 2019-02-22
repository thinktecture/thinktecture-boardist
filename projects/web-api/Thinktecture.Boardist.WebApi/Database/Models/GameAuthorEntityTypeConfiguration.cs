using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class GameAuthorEntityTypeConfiguration : IEntityTypeConfiguration<GameAuthor>
  {
    public void Configure(EntityTypeBuilder<GameAuthor> builder)
    {
      builder.HasKey(p => new {p.GameId, p.AuthorId});

      builder
        .HasOne(p => p.Game)
        .WithMany(p => p.Authors)
        .HasForeignKey(p => p.GameId);

      builder
        .HasOne(p => p.Author)
        .WithMany()
        .HasForeignKey(p => p.AuthorId);
    }
  }
}
