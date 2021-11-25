using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarListController : Controller
    {
        private ICarRepository _carRepository;
        
        public CarListController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCarList()
        {
            var cars = await _carRepository.GetCarsAsync();
            return Ok(new {carCount = cars.Length, cars});
        }
    }
}