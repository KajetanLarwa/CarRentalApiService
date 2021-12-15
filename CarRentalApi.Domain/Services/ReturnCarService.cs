using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;

namespace CarRentalApi.Domain.Services
{
    public class ReturnCarService : IReturnCarUseCase
    {
        private readonly ICarRepository _carRepository;
        private const int NotFoundCode = 404;
        private const int ConflictCode = 409;
        private const int SuccessCode = 200;
        
        public ReturnCarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        public async Task<int> ReturnCarAsync(ReturnCarRequest request)
        {
            var reservationId = request.ReservationId;
            var reservation = _carRepository.GetReservationById(reservationId);
            if (reservation == null)
                return NotFoundCode;
            if(!_carRepository.UpdateReservationToReturned(reservation.Result))
                return ConflictCode;
            return SuccessCode;
        }
    }
}