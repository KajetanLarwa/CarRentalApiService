using System.Runtime.Versioning;
using System.Threading.Tasks;
using CarRentalApi.Domain;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentCarController : Controller
    {
        private readonly IRentCarUseCase _rentCarUseCase;
        private const int SuccessCode = 200;
        private const int BadRequestCode = 400;
        private const int NotFoundCode = 404;
        private const int ConflictCode = 409;
        
        public RentCarController(IRentCarUseCase rentCarUseCase)
        {
            _rentCarUseCase = rentCarUseCase;
        }
        
        /// <summary>
        /// Add car rent reservation at specified date
        /// </summary>
        /// <response code="200">Successfully rented</response>
        /// <response code="400">Incorrect date</response>
        /// <response code="404">Incorrect car id</response>
        /// <response code="409">Car already booked at specified date</response>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> RentCar([FromBody] RentCarRequest request)
        {
            var (statusCode, reservation) = await _rentCarUseCase.RentCarAsync(request);

            return statusCode switch
            {
                SuccessCode => Ok(new RentCarResponse()
                {
                    PriceId = reservation.PriceId,
                    RentId = reservation.ID,
                    RentAt = reservation.ReservedAt,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Error = null
                }),
                BadRequestCode => BadRequest(new RentCarResponse()
                {
                    Error = "Incorrect date"
                }),
                NotFoundCode => NotFound(new RentCarResponse()
                {
                    Error = "Cannot rent car, incorrect car id or price id"
                }),
                ConflictCode => Conflict(new RentCarResponse()
                {
                    Error = "Car already booked at specified date"
                }),
                _ => NotFound(new RentCarResponse()
                {
                    Error = "Unknown error"
                })
            };
        }
    }
}