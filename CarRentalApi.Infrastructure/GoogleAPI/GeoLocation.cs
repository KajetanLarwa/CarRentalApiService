using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Domain.Ports.Out;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;


namespace CarRentalApi.Infrastructure.GoogleAPI
{
    public class GeoLocation: IGeoLocator
    {
        private readonly GoogleApiConfig _apiConfig;
        
        public GeoLocation(GoogleApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
        }
        
        public async Task<(double Latitude, double Longitude)> GetGeoLocationAsync(string country, string city)
        {
            var geoCodeReq = new GeocodingRequest()
            {
                ApiKey = _apiConfig.ApiKey,
                Address = $"{country} {city}"
            };
            var geoCodeEngine = GoogleMaps.Geocode;
            var geoCode = await geoCodeEngine.QueryAsync(geoCodeReq);
            var location = geoCode.Results.First().Geometry.Location;
            
            return (location.Latitude, location.Longitude);
        }
    }
    
}