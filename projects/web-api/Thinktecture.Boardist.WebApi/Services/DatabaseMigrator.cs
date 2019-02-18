using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Thinktecture.Boardist.WebApi.Database;

namespace Thinktecture.Boardist.WebApi.Services
{
  public class DatabaseMigrator
  {
    private readonly BoardistContext _boardistContext;

    public DatabaseMigrator(BoardistContext boardistContext)
    {
      _boardistContext = boardistContext;
    }

    public void Migrate()
    {
      var migrator = _boardistContext.Database.GetService<IMigrator>();
      migrator.Migrate();
    }
  }
}
