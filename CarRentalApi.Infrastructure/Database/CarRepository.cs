﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Domain.Do;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Infrastructure.Database
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _context;

        public CarRepository(CarRentalContext carRentalContext)
        {
            _context = carRentalContext;
        }

        public Price PutPrice(double priceValue, string currency, DateTime generatedAt, DateTime expiredAt)
        {
            var price = new Price(){Value = priceValue, Currency = currency, GeneratedAt = generatedAt, ExpiredAt = expiredAt};
            _context.Prices.Add(price);
            var priceId = _context.Prices.Last().ID;
            _context.SaveChanges();
            return price;
        }
        

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            return await _context.Cars.FindAsync(carId);
        }
        
        public async Task<Car> GetCarByNameAsync(string brand, string model)
        {
            return await _context.Cars.FirstAsync(car => car.CarModel.Brand == brand && car.CarModel.Model == model);
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            var query = _context.Cars
                .Include(c => c.CarModel)
                .Include(c => c.Category);
            return await query.ToListAsync();
        }

        public async Task<Reservation?> PutReservation(int carId, int priceId, DateTime reservedAt, DateTime from, DateTime to)
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
                PriceId = priceId,
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
        
        public async Task<Reservation?> GetReservationById(long reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            return reservation;
        }

        public async Task<Price> GetPriceById(long priceId)
        {
            var price = await _context.Prices.FindAsync(priceId);
            return price;
        }
    }
}