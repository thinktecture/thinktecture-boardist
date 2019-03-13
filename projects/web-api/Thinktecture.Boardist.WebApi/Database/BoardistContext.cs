using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Boardist.WebApi.Database.Models;

namespace Thinktecture.Boardist.WebApi.Database
{
  public class BoardistContext : DbContext
  {
    public static readonly int MaxStringLength = 250;

    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Mechanic> Mechanics { get; set; }

    private DbQuery<DbQueryValue> DbQueryValue { get; set; }

    public BoardistContext(DbContextOptions<BoardistContext> options)
      : base(options)
    {
    }

    public async Task<byte[]> GetMinActiveRowVersionAsync()
    {
      return await DbQueryValue.FromSql("SELECT MIN_ACTIVE_ROWVERSION() AS Value").Select(p => p.Value).FirstAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new GameEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new PublisherEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new MechanicEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new GameMechanicEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new GameAuthorEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new GameIllustratorEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new GameCategoryEntityTypeConfiguration());
    }
  }
}
