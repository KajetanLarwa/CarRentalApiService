using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Domain.Services
{
    public class RentCarService : IRentCarUseCase
    {
        private readonly ICarRepository _carRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IReservationRepository _reservationRepository;

        public RentCarService(ICarRepository carRepository, IReservationRepository reservationRepository, IPriceRepository priceRepository)
        {
            _carRepository = carRepository;
            _reservationRepository = reservationRepository;
            _priceRepository = priceRepository;
        }
        
        public async Task<(ActionResultCode, RentCarResponse)> RentCarAsync(RentCarRequest request, long carId)
        {
            if (await _carRepository.GetCarByIdAsync(carId) == null)
                return (ActionResultCode.NotFound, null);
            
            var price = await _priceRepository.GetPriceById(request.PriceId);
            
            if (price == null)
                return (ActionResultCode.NotFound, null);
            if (price.ExpiredAt < DateTime.Now)
                return (ActionResultCode.Gone, null);
            if (request.StartDate > request.EndDate || DateTime.Now > request.StartDate || Convert.ToInt32((request.EndDate - request.StartDate).TotalDays) != price.DaysCount)
                return (ActionResultCode.BadRequest, null);
            
            var reservation = await _reservationRepository.PutReservation(carId, request.PriceId, DateTime.Now, request.StartDate, request.EndDate);

            if (reservation == null)
                return (ActionResultCode.Conflict, null);

            var reservationData = new RentCarResponse()
            {
                PriceId = reservation.PriceID,
                RentId = reservation.ID,
                RentAt = reservation.ReservedAt,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate
            };
            
            return (ActionResultCode.Ok, reservationData);
        }
    }
}