using System.Linq;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarRentalApi.Infrastructure.Test.DatabaseTest
{
    public class DatabaseTest
    {
        private readonly ConnectionFactory factory;
        private readonly CarRentalContext context;
        private readonly Car[] carsWithoutRelation;
        private readonly Car[] cars;
        public DatabaseTest()
        {
            factory = new ConnectionFactory();
            context = factory.CreateContext();
            carsWithoutRelation = new Car[]
            {
                new Car() {BasePrice = 30, Capacity = 5, Latitude = 34.54, Longitude = 78.32, ProductionYear = 2000},
                new Car() {BasePrice = 10, Capacity = 5, Latitude = 44.54, Longitude = 38.32, ProductionYear = 2012},
                new Car() {BasePrice = 20, Capacity = 5, Latitude = 49.41, Longitude = 33.2, ProductionYear = 2007}
            };
            cars = new Car[]
            {
                new Car() {ID = 1, CarModelID = 1, CategoryID = 1, BasePrice = 30, Capacity = 5, Latitude = 34.54, Longitude = 78.32, ProductionYear = 2000},
                new Car() {ID = 2, CarModelID = 2, CategoryID = 2, BasePrice = 10, Capacity = 5, Latitude = 44.54, Longitude = 38.32, ProductionYear = 2012},
                new Car() {ID = 3, CarModelID = 2, CategoryID = 1, BasePrice = 20, Capacity = 5, Latitude = 49.41, Longitude = 33.2, ProductionYear = 2007}
            };    
        }
        [Fact]
        public void Task_Add_Without_Relation()
        {
            context.Cars.AddRange(carsWithoutRelation);

            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.Empty(context.Cars.ToList());
        }

        [Fact]
        public void Task_Add_With_Relation_ReturnException()
        {
            context.Cars.AddRange(cars);

            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.Empty(context.Cars.ToList());
        }

        [Fact]
        public void Task_Add_With_Relation_Return_No_Exception()
        {
            var carModels = new CarModel[]
            {
                new CarModel("Ford", "Focus"),
                new CarModel("Opel", "Corsa"),
            };

            var carCategories = new Category[]
            {
                new Category("small"),
                new Category("medium"),
            };

            context.Categories.AddRange(carCategories);
            context.CarModels.AddRange(carModels);
            context.SaveChanges();
            
            context.Cars.AddRange(cars);
             context.SaveChanges();
            
            var carsCount = context.Cars.Count();
            if (carsCount != 0)
            {
                Assert.Equal(3, carsCount);
            }
            
            var carsFromDb = context.Cars.ToList();
            if (carsFromDb != null)
            {
                for (int i = 0; i < carsCount; i++)
                {
                    Assert.Equal(cars[i].CategoryID, carsFromDb[i].CategoryID);
                    Assert.Equal(cars[i].CarModelID, carsFromDb[i].CarModelID);
                    Assert.Equal(cars[i].Capacity, carsFromDb[i].Capacity);
                    Assert.Equal(cars[i].ProductionYear, carsFromDb[i].ProductionYear);
                    Assert.Equal(cars[i].BasePrice, carsFromDb[i].BasePrice);
                    Assert.Equal(cars[i].Latitude, carsFromDb[i].Latitude);
                    Assert.Equal(cars[i].Longitude, carsFromDb[i].Longitude);
                }
            }
        }
    }
}