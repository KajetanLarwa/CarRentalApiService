using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;

namespace CarRentalApi.Domain.Ports.In
{
    public interface IReturnCarUseCase
    {
        Task<ActionResultCode> ReturnCarAsync(long carId);
    }
}