using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Infrastructure.Database
{
    public class CarRepository : ICarRepository
    {
        private CarRentalContext context;

        public CarRepository(CarRentalContext carRentalContext)
        {
            context = carRentalContext;
        }

        public async Task<Car> GetCarById(int carId)
        {
            return await context.Cars.FindAsync(carId);
        }

        public async Task<ExportCar[]> GetCarsAsync()
        {
            var query = from car in context.Cars
                join category in context.Categories on car.CategoryID equals category.ID
                join model in context.CarModels on car.CarModelID equals model.ID
                select new ExportCar()
                {
                    Id = car.ID,
                    Brand = model.Brand,
                    Model = model.Model,
                    ProductionYear = car.ProductionYear,
                    Capacity = car.Capacity,
                    Category = category.Name
                };
            return await query.ToArrayAsync();
        }
    }
}