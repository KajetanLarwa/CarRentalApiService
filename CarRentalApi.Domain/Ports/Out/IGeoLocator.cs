using System.Threading.Tasks;

namespace CarRentalApi.Domain.Ports.Out
{
    public interface IGeoLocator
    {
        Task<(double Latitude, double Longitude)> GetGeoLocationAsync(string country, string city);
    }
}