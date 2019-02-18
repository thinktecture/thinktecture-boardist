using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.Database
{
  public class BoardistContext : DbContext
  {
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }

    public BoardistContext(DbContextOptions<BoardistContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Category>().HasKey(p => p.Id);

      modelBuilder.Entity<Game>().HasKey(p => p.Id);

      modelBuilder.Entity<Game>().HasOne(p => p.Publisher);

      modelBuilder.Entity<Game>()
        .HasOne(p => p.MainGame)
        .WithOne()
        .IsRequired(false);

      modelBuilder.Entity<Game>().HasMany(p => p.Categories).WithOne();

      modelBuilder.Entity<Game>().Property(p => p.BuyPrice).HasColumnType("decimal(3,2)");

      modelBuilder.Entity<GameAuthor>().HasKey(p => new {p.GameId, p.AuthorId});

      modelBuilder.Entity<GameAuthor>()
        .HasOne(p => p.Game)
        .WithMany(p => p.Authors)
        .HasForeignKey(p => p.GameId);

      modelBuilder.Entity<GameAuthor>()
        .HasOne(p => p.Author)
        .WithMany()
        .HasForeignKey(p => p.AuthorId);

      modelBuilder.Entity<GameIllustrator>().HasKey(p => new {p.GameId, p.IllustratorId});

      modelBuilder.Entity<GameIllustrator>()
        .HasOne(p => p.Game)
        .WithMany(p => p.Illustrators)
        .HasForeignKey(p => p.GameId);

      modelBuilder.Entity<GameIllustrator>()
        .HasOne(p => p.Illustrator)
        .WithMany()
        .HasForeignKey(p => p.IllustratorId);

      modelBuilder.Entity<Person>().HasKey(p => p.Id);

      modelBuilder.Entity<Publisher>().HasKey(p => p.Id);
    }
  }
}
