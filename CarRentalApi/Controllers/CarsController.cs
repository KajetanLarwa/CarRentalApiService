using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly IGetCarsUseCase _getCarsUseCase;

        public CarsController(IGetCarsUseCase getCarsUseCase)
        {
            _getCarsUseCase = getCarsUseCase;
        }
        
        /// <summary>
        /// Gets the list of all cars that are offered by this company
        /// </summary>
        /// <response code="200">List returned</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Produces("application/json")]
        [ApiExplorerSettings(GroupName = "Cars")]
        [ProducesResponseType(typeof(CarsResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        public async Task<ActionResult> GetCarList()
        {
            var cars = await _getCarsUseCase.GetAllCarsAsync();
            return Ok(new CarsResponse() {CarCount = cars.Count, Cars = cars});
        }

        public class CarsResponse
        {
            [JsonPropertyName("carCount")]
            [Required, Range(0, long.MaxValue)]
            public int CarCount { get; set; }
            [Required]
            [JsonPropertyName("cars")]
            public List<CarDetails> Cars { get; set; }
        }
    }
}