using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;

namespace CarRentalApi.Domain.Services
{
    public class ReturnCarService : IReturnCarUseCase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReturnCarService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ActionResultCode> ReturnCarAsync(long reservationId)
        {
            var reservation = await _reservationRepository.GetReservationById(reservationId);

            if (reservation == null)
                return ActionResultCode.NotFound;
            if (!_reservationRepository.UpdateReservationToReturned(reservation))
                return ActionResultCode.Conflict;
            return ActionResultCode.Ok;
        }
    }
}