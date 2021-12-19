using System;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;

namespace CarRentalApi.Domain.Services
{
    public class PriceService: ICheckPriceUseCase
    {
        private readonly PriceCalculator _priceCalculator;
        private readonly ICarRepository _carRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IGeoLocator _geoLocator;

        public PriceService(PriceCalculator priceCalculator, ICarRepository carRepository, IGeoLocator geoLocator, IPriceRepository priceRepository)
        {
            _priceCalculator = priceCalculator;
            _carRepository = carRepository;
            _geoLocator = geoLocator;
            _priceRepository = priceRepository;
        }

        public async Task<(ActionResultCode, CarPrice)> CheckPriceAsync(CheckPriceRequest request, long carId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            return car == null ? (ActionResultCode.NotFound, null) : await CalculatePrice(request, car);
        }
        
        public async Task<(ActionResultCode, CarPrice)> CheckPriceAsync(CheckPriceRequest request, string brand, string model)
        {
            var car = await _carRepository.GetCarByBrandAndModelAsync(brand, model);

            return car == null ? (ActionResultCode.NotFound, null) : await CalculatePrice(request, car);
        }

        private async Task<(ActionResultCode, CarPrice)> CalculatePrice(CheckPriceRequest request, Car car)
        {
            try
            {
                var (latitude, longitude) = await _geoLocator.GetGeoLocationAsync(request.Country, request.City);

                var priceEstimate = _priceCalculator.Calculate(car, request.YearsOfHavingLicense, request.Age, latitude,
                    longitude, request.CurrentlyRentedCount, request.OverallRentedCount, request.DaysCount);

                var generatedAt = DateTime.Now;
                var expiredAt = generatedAt.AddHours(1);
                
                var price = _priceRepository.PutPrice(priceEstimate, "PLN", request.DaysCount, generatedAt, expiredAt);

                var priceDto = new CarPrice()
                {
                    Id = price.ID,
                    Price = priceEstimate,
                    ExpiredAt = price.ExpiredAt,
                    GeneratedAt = price.GeneratedAt,
                    Currency = price.Currency
                };

                return (ActionResultCode.Ok, priceDto);
            }
            catch (Exception ex)
            {
                return (ActionResultCode.BadRequest, null);
            }
        }

    }
}