using System;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.Out
{
    public interface IReservationRepository
    {
        Task<Reservation> PutReservation(long carId, long priceId, DateTime reservedAt, DateTime from, DateTime to);
        bool UpdateReservationToReturned(Reservation reservationId);
        Task<Reservation> GetReservationById(long reservationId);
    }
}