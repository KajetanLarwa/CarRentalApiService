using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.Out
{
    public interface ICarRepository
    {
        Price PutPrice(double priceValue, string currency, DateTime generatedAt, DateTime expiredAt);
        Task<Car> GetCarByIdAsync(int carId);
        Task<Car> GetCarByNameAsync(string brand, string model);
        Task<List<Car>> GetCarsAsync();
        Task<Reservation?> PutReservation(int carId, int priceId, DateTime reservedAt, DateTime from, DateTime to);
        bool UpdateReservationToReturned(Reservation reservationId);
        Task<Reservation> GetReservationById(long reservationId);
        Task<Price> GetPriceById(long priceId);
    }
}