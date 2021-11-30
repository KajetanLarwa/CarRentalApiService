using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Domain.Do;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;

namespace CarRentalApi.Domain.Services
{
    public class CarService: IGetCarsUseCase
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarDetails>> GetAllCarsAsync()
        {
            return (await _carRepository.GetCarsAsync()).Select(c => c.ToDto()).ToList();
        }
    }
}