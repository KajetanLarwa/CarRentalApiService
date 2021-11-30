using System.Threading.Tasks;

namespace CarRentalApi.Domain.Ports.In
{
    public interface ICheckPriceUseCase
    {
        Task<double?> CheckPriceAsync(CheckPriceRequest request);
    }
}