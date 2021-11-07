using System.Threading.Tasks;

namespace CarRentalApi.Domain.Entity
{
    public interface ICarRepository
    {
        Task<Car> GetCarById(int carId);
    }
}