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
      CreateGameMechanicModel(modelBuilder);
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
    
    private static void CreateGameMechanicModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<GameMechanic>().HasKey(p => new {p.GameId, p.MechanicId});

      modelBuilder.Entity<GameMechanic>()
        .HasOne(p => p.Game)
        .WithMany(p => p.Mechanics)
        .HasForeignKey(p => p.GameId);

      modelBuilder.Entity<GameMechanic>()
        .HasOne(p => p.Mechanic)
        .WithMany()
        .HasForeignKey(p => p.MechanicId);
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
  }
}
