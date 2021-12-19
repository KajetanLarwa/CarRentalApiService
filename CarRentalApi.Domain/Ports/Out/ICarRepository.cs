using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.Out
{
    public interface ICarRepository
    {
        Task<Car> GetCarByIdAsync(long carId);
        Task<Car> GetCarByBrandAndModelAsync(string brand, string model);
        Task<List<Car>> GetCarsAsync();
    }
}