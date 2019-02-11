using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Api;
using Workflow.Core;
using Workflow.Data;
using Workflow.Data.DataClass;
using Workflow.WebUi.Helpers;

namespace Workflow.WebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });

            services.AddScoped<DbSeeder>();
            services.AddScoped<IWorkflowDataService, WorkflowDataService>();
            services.AddScoped<IWorkflowManager, WorkflowManager>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbSeeder dbseeder)
        {
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();

      //handle client side orutes
      //https://weblog.west-wind.com/posts/2017/Aug/07/Handling-HTML5-Client-Route-Fallbacks-in-ASPNET-Core
      app.Run(async (context) =>
          {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
          });

            dbseeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}
