using System;
using CarRentalApi.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRentalApi
{
    public class Startup
    {
        private IWebHostEnvironment CurrentEnvironment{ get; set; } 
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            CurrentEnvironment = env;
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var envName = CurrentEnvironment.EnvironmentName;
            var dbConfig = Configuration.GetSection("DataBase");
            var connectionString = envName switch
            {
                "Production" => GetConnectionString(dbConfig.GetSection("Production")),
                "Development" => GetConnectionString(dbConfig.GetSection("Development")),
                "DockerCompose" => GetConnectionString(dbConfig.GetSection("DockerCompose"))
            };

            services.AddDbContext<CarRentalContext>(options => options.UseSqlServer(connectionString));
        }

        private string GetConnectionString(IConfiguration config)
        {
            var server = config["server"];
            var dbName = config["DatabaseName"];
            var userName = Configuration[config["SecretLogin"]];
            var password = Configuration[config["SecretPassword"]];
            var connectionString = $"Server={server}; Database={dbName}; User Id={userName}; Password={password}; Trusted_Connection=false;";
            Console.WriteLine(connectionString);
            return connectionString;
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CurrentEnvironment = env;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
