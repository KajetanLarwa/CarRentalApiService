using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.In
{
    public interface ICheckPriceUseCase
    {
        Task<(ActionResultCode, CarPrice)> CheckPriceAsync(CheckPriceRequest request, string brand, string model);
        Task<(ActionResultCode, CarPrice)> CheckPriceAsync(CheckPriceRequest request, long carId);
    }
}