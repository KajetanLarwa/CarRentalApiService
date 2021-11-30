using System;
using System.Threading.Tasks;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;
using CarRentalApi.Domain.Services;

namespace CarRentalApi.Domain
{
    public class PriceService: ICheckPriceUseCase
    {
        private readonly PriceCalculator _priceCalculator;
        private readonly ICarRepository _carRepository;
        private readonly IGeoLocator _geoLocator;

        public PriceService(PriceCalculator priceCalculator, ICarRepository carRepository, IGeoLocator geoLocator)
        {
            _priceCalculator = priceCalculator;
            _carRepository = carRepository;
            _geoLocator = geoLocator;
        }

        public async Task<double?> CheckPriceAsync(CheckPriceRequest request)
        {
            var carId = request.CarId;
            var car = await _carRepository.GetCarByIdAsync(carId);

            if (car == null)
            {
                return null;
            }
            
            try
            {
                var (latitude, longitude) = await _geoLocator.GetGeoLocationAsync(request.Country, request.City);
                
                return _priceCalculator.Calculate(car, request.YearsOfHavingLicense, request.Age, latitude, longitude,
                    request.CurrentlyRentedCount, request.OverallRentedCount);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}