using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReturnCarController : Controller
    {
        private readonly IReturnCarUseCase _returnCarUseCase;
        public ReturnCarController(IReturnCarUseCase returnCarUseCase)
        {
            _returnCarUseCase = returnCarUseCase;
        }
        
        /// <summary>
        /// Updates reservation status to car returned
        /// </summary>
        /// <response code="200">Successfully updated reservation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Incorrect reservation id</response>
        /// <response code="409">Car already returned</response>
        [HttpPost("{reservationId:long}/return")]
        [Produces("application/json")]
        [ApiExplorerSettings(GroupName = "Reservations")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 409)]
        public async Task<ActionResult> ReturnCar(long reservationId)
        {
            var result = await _returnCarUseCase.ReturnCarAsync(reservationId);
            return result switch
            {
                ActionResultCode.Ok => Ok(new ApiResponse() { Message = "Successfully returned the car" }),
                ActionResultCode.NotFound => NotFound(new ApiErrorResponse() { Error = "Cannot return car, incorrect reservation" }),
                ActionResultCode.Conflict => Conflict(new ApiErrorResponse() { Error = "Car already returned" }),
                _ => StatusCode(500)
            };
        }
    }
}