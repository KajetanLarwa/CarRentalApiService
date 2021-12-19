using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.In
{
    public interface IRentCarUseCase
    {
        Task<(ActionResultCode, RentCarResponse)> RentCarAsync(RentCarRequest request, long carId);
    }
}