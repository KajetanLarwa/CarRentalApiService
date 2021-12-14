using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Domain.Ports.Out;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Domain.Services
{
    public class RentCarService : IRentCarUseCase
    {
        private readonly ICarRepository _carRepository;
        private const int SuccessCode = 200;
        private const int BadRequestCode = 400;
        private const int NotFoundCode = 404;
        private const int ConflictCode = 409;

        public RentCarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        public async Task<int> RentCarAsync(RentCarRequest request)
        {
            var carId = request.CarId;
            if (await _carRepository.GetCarByIdAsync(carId) == null)
                return NotFoundCode;
            var start = request.StartDate;
            var end = request.EndDate;
            if (start > end || DateTime.Now > start)
                return BadRequestCode;
            var isSuccess = await _carRepository.PutReservation(carId, start, end);
            return !isSuccess ? ConflictCode : SuccessCode;
        }
    }
}