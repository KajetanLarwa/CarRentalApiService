using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.Out
{
    public interface ICarRepository
    {
        Task<Car> GetCarByIdAsync(int carId);
        Task<List<Car>> GetCarsAsync();
    }
}