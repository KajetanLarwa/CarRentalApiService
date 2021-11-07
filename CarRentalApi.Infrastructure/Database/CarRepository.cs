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
    }
}