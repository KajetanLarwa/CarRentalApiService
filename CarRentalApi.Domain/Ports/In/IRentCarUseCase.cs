using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.In
{
    public interface IRentCarUseCase
    {
        Task<(int, Reservation?)> RentCarAsync(RentCarRequest request);
    }
}