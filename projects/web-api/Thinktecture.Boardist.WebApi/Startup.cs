using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thinktecture.Boardist.WebApi.Database;
using Thinktecture.Boardist.WebApi.Services;

namespace Thinktecture.Boardist.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<BoardistContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BoardistContext")));

      services.AddTransient<DatabaseMigrator>();

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        var migrator = scope.ServiceProvider.GetRequiredService<DatabaseMigrator>();
        migrator.Migrate();
      }

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // TODO: DO NOT USE IN PRODUCTION
      app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

      app.UseMvc();
    }
  }
}
