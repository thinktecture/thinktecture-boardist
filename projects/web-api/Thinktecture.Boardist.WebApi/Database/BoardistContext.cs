using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database.Models;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi.Database
{
  public class BoardistContext : DbContext
  {
    private const int MAX_STRING_LENGTH = 250;
    
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

      CreateCategoryModel(modelBuilder);
      CreateGameModel(modelBuilder);
      CreateGameAuthorModel(modelBuilder);
      CreateGameIllustrator(modelBuilder);
      CreateGameCategoryModel(modelBuilder);
      CreatePersonModel(modelBuilder);
      CreatePublisherModel(modelBuilder);

      SeedDatabase(modelBuilder);
    }

    private static void CreatePublisherModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Publisher>().HasKey(p => p.Id);
      modelBuilder.Entity<Publisher>().Property(p => p.Name).HasMaxLength(MAX_STRING_LENGTH);
    }

    private static void CreatePersonModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Person>().HasKey(p => p.Id);
      modelBuilder.Entity<Person>().Property(p => p.FirstName).HasMaxLength(MAX_STRING_LENGTH);
      modelBuilder.Entity<Person>().Property(p => p.LastName).HasMaxLength(MAX_STRING_LENGTH);
    }

    private static void CreateGameCategoryModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<GameCategory>().HasKey(p => new {p.GameId, p.CategoryId});

      modelBuilder.Entity<GameCategory>()
        .HasOne(p => p.Game)
        .WithMany(p => p.Categories)
        .HasForeignKey(p => p.GameId);

      modelBuilder.Entity<GameCategory>()
        .HasOne(p => p.Category)
        .WithMany()
        .HasForeignKey(p => p.CategoryId);
    }

    private static void CreateGameIllustrator(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<GameIllustrator>().HasKey(p => new {p.GameId, p.IllustratorId});

      modelBuilder.Entity<GameIllustrator>()
        .HasOne(p => p.Game)
        .WithMany(p => p.Illustrators)
        .HasForeignKey(p => p.GameId);

      modelBuilder.Entity<GameIllustrator>()
        .HasOne(p => p.Illustrator)
        .WithMany()
        .HasForeignKey(p => p.IllustratorId);
    }

    private static void CreateGameAuthorModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<GameAuthor>().HasKey(p => new {p.GameId, p.AuthorId});

      modelBuilder.Entity<GameAuthor>()
        .HasOne(p => p.Game)
        .WithMany(p => p.Authors)
        .HasForeignKey(p => p.GameId);

      modelBuilder.Entity<GameAuthor>()
        .HasOne(p => p.Author)
        .WithMany()
        .HasForeignKey(p => p.AuthorId);
    }

    private static void CreateGameModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Game>().HasKey(p => p.Id);
      modelBuilder.Entity<Game>().Property(p => p.Name).HasMaxLength(MAX_STRING_LENGTH);

      modelBuilder.Entity<Game>()
        .HasOne(p => p.Publisher)
        .WithMany()
        .HasForeignKey(p => p.PublisherId)
        .IsRequired();

      modelBuilder.Entity<Game>()
        .HasOne(p => p.MainGame)
        .WithMany()
        .HasForeignKey(p => p.MainGameId)
        .IsRequired(false);

      modelBuilder.Entity<Game>().HasMany(p => p.Categories).WithOne();

      modelBuilder.Entity<Game>().Property(p => p.BuyPrice).HasColumnType("decimal(3,2)");
    }

    private static void CreateCategoryModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>().HasKey(p => p.Id);
      modelBuilder.Entity<Category>().Property(p => p.Name).HasMaxLength(MAX_STRING_LENGTH);
    }

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
      var persons = new List<Person>()
      {
        new Person() {Id = Guid.Parse("6d6e4795-fd8d-4630-add0-eb80cc2c7fb2"), LastName = "Bauza", FirstName = "Antoine"},
        new Person() {Id = Guid.Parse("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50c"), LastName = "Michael", FirstName = "Menzel"},
        new Person() {Id = Guid.Parse("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50d"), LastName = "Herbert", FirstName = "Grönemeyer"},
        new Person() {Id = Guid.Parse("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e"), LastName = "Lucialla", FirstName = "Löhr"}
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

      var illustratorRelationships = new List<GameIllustrator>()
      {
        new GameIllustrator()
        {
          IllustratorId = persons[2].Id,
          GameId = sevenWonders.Id
        },
        new GameIllustrator()
        {
          IllustratorId = persons[3].Id,
          GameId = sevenWonders.Id
        },
        new GameIllustrator()
        {
          IllustratorId = persons[2].Id,
          GameId = sevenWondersBabel.Id
        },
        new GameIllustrator()
        {
          IllustratorId = persons[3].Id,
          GameId = sevenWondersBabel.Id
        },
        new GameIllustrator()
        {
          IllustratorId = persons[3].Id,
          GameId = legendenVonAndor.Id
        },
      };

      var categories = new List<Category>()
      {
        new Category()
        {
          Id = Guid.Parse("23d3a212-4996-4fe2-a3fa-d9fe8b975952"),
          Name = "Roleplay Game"
        },
        new Category()
        {
          Id = Guid.Parse("75590c0d-46c6-4db3-a772-6614b6354c71"),
          Name = "Card Game"
        },
        new Category()
        {
          Id = Guid.Parse("631caec4-088d-4cce-baa8-3356302b76da"),
          Name = "City Building"
        },
        new Category()
        {
          Id = Guid.Parse("537d56da-6a38-4da8-b872-ec462d5ef512"),
          Name = "Civilization"
        }
      };

      var categoriesRelationships = new List<GameCategory>()
      {
        new GameCategory()
        {
          GameId = sevenWonders.Id,
          CategoryId = categories[1].Id
        },
        new GameCategory()
        {
          GameId = sevenWonders.Id,
          CategoryId = categories[2].Id
        },
        new GameCategory()
        {
          GameId = sevenWonders.Id,
          CategoryId = categories[3].Id
        },
        new GameCategory()
        {
          GameId = sevenWondersBabel.Id,
          CategoryId = categories[1].Id
        },
        new GameCategory()
        {
          GameId = sevenWondersBabel.Id,
          CategoryId = categories[2].Id
        },
        new GameCategory()
        {
          GameId = sevenWondersBabel.Id,
          CategoryId = categories[3].Id
        },
        new GameCategory()
        {
          GameId = legendenVonAndor.Id,
          CategoryId = categories[0].Id
        }
      };
      
      var games = new List<Game>() {sevenWonders, sevenWondersBabel, legendenVonAndor};

      modelBuilder.Entity<Publisher>().HasData(publishers);
      modelBuilder.Entity<Person>().HasData(persons);
      modelBuilder.Entity<Game>().HasData(games);
      modelBuilder.Entity<Category>().HasData(categories);
      modelBuilder.Entity<GameAuthor>().HasData(authorRelationships);
      modelBuilder.Entity<GameCategory>().HasData(categoriesRelationships);
      modelBuilder.Entity<GameIllustrator>().HasData(illustratorRelationships);
    }
  }
}
