using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class RentCarController : Controller
    {
        private readonly IRentCarUseCase _rentCarUseCase;

        public RentCarController(IRentCarUseCase rentCarUseCase)
        {
            _rentCarUseCase = rentCarUseCase;
        }

        /// <summary>
        /// Add car rent reservation at specified date
        /// </summary>
        /// <response code="200">Successfully rented</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="400">Incorrect date</response>
        /// <response code="404">Incorrect car id</response>
        /// <response code="409">Car already booked at specified date</response>
        [HttpPost("{carId:long}/rent")]
        [Produces("application/json")]
        [ApiExplorerSettings(GroupName = "Cars")]
        [ProducesResponseType(typeof(RentCarResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 409)]
        public async Task<ActionResult<ApiResponse>> RentCar([FromBody] RentCarRequest request, long carId)
        {
            var (statusCode, reservationData) = await _rentCarUseCase.RentCarAsync(request, carId);

            return statusCode switch
            {
                ActionResultCode.Ok => Ok(reservationData),
                ActionResultCode.BadRequest => BadRequest(new ApiErrorResponse() {Error = "Incorrect date"}),
                ActionResultCode.NotFound => NotFound(new ApiErrorResponse()
                    {Error = "Cannot rent car, incorrect car id or price id"}),
                ActionResultCode.Conflict => Conflict(new ApiErrorResponse()
                    {Error = "Car already booked at specified date"}),
                _ => StatusCode(500)
            };
        }
    }
}