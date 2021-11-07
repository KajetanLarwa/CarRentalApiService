using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPriceController : ControllerBase
    {
        private ICarRepository CarRepository;
        private GeoLocation GeoLocator;
        private PriceCalculator PriceCalculator;
        
        public CheckPriceController(ICarRepository carRepository, GeoLocation geoLocator, PriceCalculator priceCalculator)
        {
            CarRepository = carRepository;
            GeoLocator = geoLocator;
            PriceCalculator = priceCalculator;
        }

        [HttpGet]
        public async Task<ActionResult<double>> Get([FromBody] CheckPriceRequest request)
        {
            var carId = request.CarId;
            var car = await CarRepository.GetCarById(carId);
            if (car == null)
            {
                var errMsg = $"[carId={carId}] invalid car id";
                return NotFound(errMsg);
            }

            double latitude;
            double longitude;
            try
            {
                (latitude, longitude) = GeoLocator.GetGeoLocation(request.Country, request.City);
            }
            catch (Exception e)
            {
                var errMsg = $"[Country={request.Country}, City={request.City}] cannot get location for" +
                             $" specified address, ex:{e}";
                return NotFound(errMsg);
            }
            var price = PriceCalculator.Calculate(car, request.YearsOfHavingLicense, request.Age, latitude, longitude,
                request.CurrentlyRentedCount, request.OverallRentedCount);
            return Ok(price);
        }
        
    }
}