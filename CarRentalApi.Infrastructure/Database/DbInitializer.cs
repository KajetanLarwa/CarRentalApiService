using System;
using System.Linq;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Infrastructure.Database
{
    public class DbInitializer
    {
        public static void Initialize(CarRentalContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Cars.Any())
            {
                return;   // DB has been seeded
            }

            var carModels = new CarModel[]
            {
                new CarModel("Ford", "Focus"),
                new CarModel("Opel", "Corsa"),
                new CarModel("Mercedes", "Vito"),
                new CarModel("Porsche", "Cayenne"),
                new CarModel("Toyota", "Supra")
            };

            var carCategories = new Category[]
            {
                new Category("small"),
                new Category("medium"),
                new Category("big"),
                new Category("xxl"),
            };

            var cars = new Car[]
            {
                new Car
                {
                    CarModelID = 1,
                    CategoryID = 2,
                    ProductionYear = 2005, 
                    Capacity = 5,
                    BasePrice = 50,
                    Latitude = 47.5675,
                    Longitude = 37.67576,
                    HorsePower = 100,
                    ProviderCompany = "DotnetRulez"
                },
                new Car
                {
                    CarModelID = 2,
                    CategoryID = 1,
                    ProductionYear = 2010, 
                    Capacity = 5,
                    BasePrice = 30,
                    Latitude = 27.345,
                    Longitude = 67.564,
                    HorsePower = 100,
                    ProviderCompany = "DotnetRulez"
                },
                new Car
                {
                    CarModelID = 3,
                    CategoryID = 4,
                    ProductionYear = 2015, 
                    Capacity = 9,
                    BasePrice = 70,
                    Latitude = 42.8976,
                    Longitude = 42.43567,
                    HorsePower = 100,
                    ProviderCompany = "DotnetRulez"
                },
                new Car
                {
                    CarModelID = 4,
                    CategoryID = 3,
                    ProductionYear = 2019, 
                    Capacity = 5,
                    BasePrice = 80,
                    Latitude = 33.346,
                    Longitude = 17.5326,
                    HorsePower = 100,
                    ProviderCompany = "DotnetRulez"
                },
                new Car
                {
                    CarModelID = 5,
                    CategoryID = 2,
                    ProductionYear = 2020, 
                    Capacity = 2,
                    BasePrice = 150,
                    Latitude = 52.5675,
                    Longitude = 21.67576,
                    HorsePower = 100,
                    ProviderCompany = "DotnetRulez"
                },
            };
            
            context.CarModels.AddRange(carModels);
            context.Categories.AddRange(carCategories);
            context.SaveChanges();
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}