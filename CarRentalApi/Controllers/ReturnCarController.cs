using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnCarController : Controller
    {
        private readonly IReturnCarUseCase _returnCarUseCase;
        private const int NotFoundCode = 404;
        private const int ConflictCode = 409;
        private const int SuccessCode = 200;
        
        public ReturnCarController(IReturnCarUseCase returnCarUseCase)
        {
            _returnCarUseCase = returnCarUseCase;
        }
        
        /// <summary>
        /// Updates reservation status to car returned
        /// </summary>
        /// <response code="200">Successfully updated reservation</response>
        /// <response code="404">Incorrect reservation id</response>
        /// <response code="409">Car already returned</response>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> ReturnCar([FromBody] ReturnCarRequest request)
        {
            var result = await _returnCarUseCase.ReturnCarAsync(request);
            return result switch
            {
                SuccessCode => Ok(new ApiResponse("Successfully returned the car")),
                NotFoundCode => NotFound(new ApiResponse("Cannot return car, incorrect reservation")),
                ConflictCode => Conflict(new ApiResponse("Car already returned")),
                _ => NotFound(new ApiResponse("Unknown error"))
            };
        }
    }
}