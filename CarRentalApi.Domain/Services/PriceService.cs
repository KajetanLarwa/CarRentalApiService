using System;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;
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

        public async Task<double?> CheckPriceAsync(CheckPriceRequestWithCarId requestWithCarId)
        {
            var carId = requestWithCarId.CarId;
            var car = await _carRepository.GetCarByIdAsync(carId);

            if (car == null)
            {
                return null;
            }
            
            try
            {
                var (latitude, longitude) = await _geoLocator.GetGeoLocationAsync(requestWithCarId.Country, requestWithCarId.City);
                
                return _priceCalculator.Calculate(car, requestWithCarId.YearsOfHavingLicense, requestWithCarId.Age, latitude, longitude,
                    requestWithCarId.CurrentlyRentedCount, requestWithCarId.OverallRentedCount);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        public async Task<double?> CheckPriceAsync(CheckPriceRequestWithCarName request)
        {
            var car = await _carRepository.GetCarByNameAsync(request.Brand, request.Model);

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

        public Price SaveCalculatedPrice(double price, string currency)
        {
            var generatedAt = DateTime.Now;
            var expiredAt = generatedAt.AddHours(1);
            return _carRepository.PutPrice(price, currency, generatedAt, expiredAt);
        }

    }
}