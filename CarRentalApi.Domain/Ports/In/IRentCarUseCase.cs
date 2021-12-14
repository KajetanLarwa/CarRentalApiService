using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;

namespace CarRentalApi.Domain.Ports.In
{
    public interface IRentCarUseCase
    {
        Task<int> RentCarAsync(RentCarRequest request);
    }
}