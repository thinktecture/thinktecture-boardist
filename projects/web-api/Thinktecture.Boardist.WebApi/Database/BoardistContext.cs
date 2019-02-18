using System;
using System.Collections.Generic;
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

      modelBuilder.Entity<Game>()
        .HasOne(p => p.Publisher)
        .WithMany()
        .HasForeignKey(p => p.PublisherId)
        .IsRequired();

      modelBuilder.Entity<Game>()
        .HasOne(p => p.MainGame)
        .WithOne()
        .HasForeignKey<Game>(p => p.MainGameId)
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

      SeedDatabase(modelBuilder);
    }
   
    private void SeedDatabase(ModelBuilder modelBuilder)
    {
      var persons = new List<Person>()
      {
        new Person() {Id = Guid.Parse("6d6e4795-fd8d-4630-add0-eb80cc2c7fb2"), LastName = "Bauza", FirstName = "Antoine"},
        new Person() {Id = Guid.Parse("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50c"), LastName = "Michael", FirstName = "Menzel"}
      };

      var publishers = new List<Publisher>()
      {
        new Publisher() {Id = Guid.Parse("e6237d73-007a-4aa5-b068-bc909f0f9897"), Name = "Asmodee"},
        new Publisher() {Id = Guid.Parse("579176ab-5eaa-484b-87fb-33806252c214"), Name = "Kosmos"},
      };

      var sevenWonders = new Game()
      {
        Id = Guid.Parse("7586c43c-ef14-499c-996b-05ad0ddecc67"),
        Name = "7 Wonders",
        PublisherId = publishers[0].Id,
        MinPlayers = 3,
        MaxPlayers = 7,
        PerPlayerDuration = 40
      };

      var sevenWondersBabel = new Game()
      {
        Id = Guid.Parse("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"),
        Name = "7 Wonders - Babel",
        PublisherId = publishers[0].Id,
        MinPlayers = 2,
        MaxPlayers = 7,
        PerPlayerDuration = 40,
        MainGameId = sevenWonders.Id
      };

      var legendenVonAndor = new Game()
      {
        Id = Guid.Parse("7e677287-e070-4ffb-b102-b45f3aeff158"),
        Name = "Die Legenden von Andor",
        PublisherId = publishers[1].Id,
        MinPlayers = 2,
        MaxPlayers = 4,
        PerPlayerDuration = 90
      };

      var authorRelationships = new List<GameAuthor>()
      {
        new GameAuthor()
        {
          AuthorId = persons[0].Id,
          GameId = sevenWonders.Id
        },
        new GameAuthor()
        {
          AuthorId = persons[0].Id,
          GameId = sevenWondersBabel.Id
        },
        new GameAuthor()
        {
          AuthorId = persons[1].Id,
          GameId = legendenVonAndor.Id
        }
      };

      var games = new List<Game>() {sevenWonders, sevenWondersBabel, legendenVonAndor};

      modelBuilder.Entity<Publisher>().HasData(publishers);
      modelBuilder.Entity<Person>().HasData(persons);
      modelBuilder.Entity<Game>().HasData(games);
      modelBuilder.Entity<GameAuthor>().HasData(authorRelationships);
    }
  }
}
