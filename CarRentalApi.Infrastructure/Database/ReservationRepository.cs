using System;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Infrastructure.Database
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly CarRentalContext _context;

        public ReservationRepository(CarRentalContext carRentalContext)
        {
            _context = carRentalContext;
        }
        
        public async Task<Reservation> PutReservation(long carId, long priceId, DateTime reservedAt, DateTime from, DateTime to)
        {
            var isReserved = await _context.Reservations.Where(r => r.CarID == carId).AnyAsync(r =>
                ((r.StartDate.CompareTo(from) <= 0 && r.EndDate.CompareTo(from) >= 0) ||
                 (r.StartDate.CompareTo(from) >= 0 && r.EndDate.CompareTo(to) <= 0) ||
                 (r.StartDate.CompareTo(to) <= 0 && r.EndDate.CompareTo(to) >= 0)) && !r.IsReturned);
            if (isReserved)
                return null;
            var reservation = new Reservation()
            {
                CarID = carId,
                PriceID = priceId,
                ReservedAt = reservedAt,
                StartDate = from,
                EndDate = to,
                IsReturned = false
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return reservation;
        }
        

        public bool UpdateReservationToReturned(Reservation reservation)
        {
            if (reservation.IsReturned)
                return false;
            reservation.IsReturned = true;
            _context.SaveChanges();
            return true;
        }
        
        public async Task<Reservation> GetReservationById(long reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            return reservation;
        }
    }
}