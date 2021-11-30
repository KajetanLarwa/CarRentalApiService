using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRental.Infrastructure.Test.Async;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.Out;
using CarRentalApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CarRentalApi.Infrastructure.Test.Database
{
    //  public class CarRepositoryTest
    // {
    //     private readonly ICarRepository _carRepository;
    //
    //     private readonly Car[] _cars =
    //     {
    //         new()
    //         {
    //             ID = 0, CarModel = new CarModel("Volkswagen", "Passat"), Capacity = 6, Category = new Category("Big"), ProductionYear = 2010, HorsePower = 150
    //         },
    //         new()
    //         {
    //             ID = 1, CarModel = new CarModel("Ford", "Mondeo"), Capacity = 6, Category = new Category("Big"), ProductionYear = 2010, HorsePower = 150
    //         },
    //     };
    //
    //     public CarRepositoryTest()
    //     {
    //         var data = _cars.AsQueryable();
    //         var mockSet = new Mock<DbSet<Car>>();
    //         mockSet.As<IAsyncEnumerable<Car>>().Setup(d => d.GetAsyncEnumerator(new CancellationToken()))
    //             .Returns(new AsyncEnumerator<Car>(data.GetEnumerator()));
    //         mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Car>(data.Provider));
    //         mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
    //         mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //         mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
    //
    //         var mockContext = new Mock<CarRentalContext>();
    //         mockContext.Setup(c => c.Cars).Returns(mockSet.Object);
    //         
    //         _carRepository = new CarRepository(mockContext.Object);
    //     }
    //     
    //     [Fact]
    //     public async Task ShouldReturnAllCarsAsync()
    //     {
    //         var result = await _carRepository.GetCarsAsync();
    //         var actualCars = result.OrderBy(c => c.ID).ToArray();
    //         Assert.Equal(_cars, actualCars);
    //     }
    //     
    //     [Fact]
    //     public async Task ShouldReturnCarByIdAsync()
    //     {
    //         var result = await _carRepository.GetCarByIdAsync(1);
    //         Assert.Equal(result, _cars[1]);
    //     }
    //     
    //    
    // }
}