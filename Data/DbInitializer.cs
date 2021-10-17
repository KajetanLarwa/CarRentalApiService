using CarRentalApiService.Models;
using System;
using System.Linq;

namespace CarRentalApiService.Data
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

            var cars = new Car[]
            {
                new Car
                {
                    Brand = "Ford", 
                    Model = "Mondeo", 
                    ProductionDate = DateTime.Parse("2005-09-01"), 
                    Type = "sedan"
                },
                new Car
                {
                    Brand = "Opel", 
                    Model = "Omega", 
                    ProductionDate = DateTime.Parse("2001-04-21"), 
                    Type = "sedan"
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    ProductionDate = DateTime.Parse("2003-02-01"),
                    Type = "hatchback"
                }
            };
            
            foreach (var c in cars)
            {
                context.Cars.Add(c);
            }
            context.SaveChanges();
        }
    }
}