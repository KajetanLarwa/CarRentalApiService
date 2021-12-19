using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;

namespace CarRentalApi.Domain.Ports.In
{
    public interface IGetCarsUseCase
    {
        Task<List<CarDetails>> GetAllCarsAsync();
    }
}