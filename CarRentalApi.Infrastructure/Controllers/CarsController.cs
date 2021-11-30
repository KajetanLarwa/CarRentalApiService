using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly IGetCarsUseCase _getCarsUseCase;

        public CarsController(IGetCarsUseCase getCarsUseCase)
        {
            _getCarsUseCase = getCarsUseCase;
        }

        [HttpGet]
        public async Task<ActionResult> GetCarList()
        {
            var cars = await _getCarsUseCase.GetAllCarsAsync();
            return Ok(new {carCount = cars.Count, cars});
        }
    }
}