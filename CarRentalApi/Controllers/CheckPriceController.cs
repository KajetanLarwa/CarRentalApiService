using System;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CheckPriceController : ControllerBase
    {
        private readonly ICheckPriceUseCase _checkPriceUseCase;

        public CheckPriceController(ICheckPriceUseCase checkPriceUseCase)
        {
            _checkPriceUseCase = checkPriceUseCase;
        }

        /// <summary>
        /// Return calculated price of renting the car by car ID
        /// </summary>
        /// <response code="200">Successfully calculated price</response>
        /// <response code="400">Cannot calculate the price</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Car not found</response>
        [HttpPost("{carId:long}/price")]
        [Produces("application/json")]
        [ApiExplorerSettings(GroupName = "Cars")]
        [ProducesResponseType(typeof(CarPrice), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<ActionResult> CheckPrice([FromBody] CheckPriceRequest request, long carId)
        {
            var result = await _checkPriceUseCase.CheckPriceAsync(request, carId);

            return GetResponseForCode(result);
        }
        
        /// <summary>
        /// Return calculated price of renting the car by car model and brand
        /// </summary>
        /// <response code="200">Successfully calculated price</response>
        /// <response code="400">Cannot calculate the price</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Car not found</response>
        [HttpPost("{brand}/{model}/price")]
        [Produces("application/json")]
        [ApiExplorerSettings(GroupName = "Cars")]
        [ProducesResponseType(typeof(CarPrice), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<ActionResult> CheckPrice([FromBody] CheckPriceRequest request, string brand, string model)
        {
            var result = await _checkPriceUseCase.CheckPriceAsync(request, brand, model);

            return GetResponseForCode(result);
        }

        private ActionResult GetResponseForCode((ActionResultCode code, CarPrice price) value)
        {
            return value.code switch
            {
                ActionResultCode.Ok => Ok(value.price),
                ActionResultCode.NotFound => NotFound(new ApiErrorResponse() { Error = "Car not found" }),
                ActionResultCode.BadRequest => BadRequest(new ApiErrorResponse() { Error = "Cannot calculate the price" }),
                _ => StatusCode(500)
            };
        }
        
    }
}