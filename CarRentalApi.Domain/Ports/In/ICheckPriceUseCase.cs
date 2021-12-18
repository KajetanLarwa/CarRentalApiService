using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.In
{
    public interface ICheckPriceUseCase
    {
        Task<double?> CheckPriceAsync(CheckPriceRequestWithCarId requestWithCarId);
        Task<double?> CheckPriceAsync(CheckPriceRequestWithCarName requestWithCarId);
        Price SaveCalculatedPrice(double price, string currency);
    }
}