using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Infrastructure.Database
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _context;

        public CarRepository(CarRentalContext carRentalContext)
        {
            _context = carRentalContext;
        }

        public async Task<Car> GetCarByIdAsync(long carId)
        {
            return await _context.Cars.FindAsync(carId);
        }
        
        public async Task<Car> GetCarByBrandAndModelAsync(string brand, string model)
        {
            return await _context.Cars.FirstAsync(car => car.CarModel.Brand == brand && car.CarModel.Model == model);
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            var query = _context.Cars
                .Include(c => c.CarModel)
                .Include(c => c.Category);
            return await query.ToListAsync();
        }
    }
}