using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddDbContext<BoardistContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BoardistContext")));

      services.AddTransient<DatabaseMigrator>();
      services.AddTransient<GamesService>();
      services.AddTransient<PublishersService>();
      services.AddTransient<CategoriesService>();
      services.AddTransient<PersonsService>();
      services.AddTransient<MechanicsService>();
      services.AddTransient<BoardGameGeekImporter>();
      services.AddTransient<FilesService>();
      services.AddTransient<SyncService>();

      services.AddHttpClient();

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

      app.UseRouting();

      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
