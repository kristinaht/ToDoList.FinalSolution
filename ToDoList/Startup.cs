using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Models;

namespace ToDoList
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json"); //we are telling our app what to do with connection string in .json file
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }
    //set; allows us to set our app's connection string

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      //Entity configuration code:
      services.AddEntityFrameworkMySql() 
        .AddDbContext<ToDoListContext>(options => options //AddDbContext is representation of our database. every time we want to reference a db in an app, we will do that by using an instance of this DbContext class.
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseStaticFiles();

      app.UseDeveloperExceptionPage();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Something went wrong!");
      });
    }
  }
}