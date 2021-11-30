using System;
using System.Diagnostics;
using CarRentalApi.Domain;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;
using CarRentalApi.Domain.Services;
using CarRentalApi.Infrastructure;
using CarRentalApi.Infrastructure.Database;
using CarRentalApi.Infrastructure.GoogleAPI;
using CarRentalApi.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

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
            var googleConfig = Configuration.GetSection("GoogleApiConfig");
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddDbContext<CarRentalContext>(options => options.UseSqlServer(connectionString));
            
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IGeoLocator, GeoLocation>(conf =>
                new GeoLocation(new GoogleApiConfig(googleConfig["ApiKey"])));
            services.AddScoped<ICheckPriceUseCase, PriceService>();
            services.AddScoped<PriceCalculator, PriceCalculator>();
            services.AddScoped<IGetCarsUseCase, CarService>();
        }

        private string GetConnectionString(IConfiguration config)
        {
            var server = config["server"];
            var dbName = config["DatabaseName"];
            var userName = Configuration[config["SecretLogin"]];
            var password = Configuration[config["SecretPassword"]];
            var connectionString = $"Server={server}; Database={dbName}; User Id={userName}; Password={password}; Trusted_Connection=false;";

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
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental v1"));

            app.UseRouting();

            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
