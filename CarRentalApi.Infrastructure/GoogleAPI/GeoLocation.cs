using System.Diagnostics;
using System.Linq;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;


namespace CarRentalApi.Infrastructure
{
    public class GeoLocation
    {
        private GoogleApiConfig ApiConfig;
        
        public GeoLocation(GoogleApiConfig apiConfig)
        {
            ApiConfig = apiConfig;
        }
        
        public (double Latitude, double Longitude) GetGeoLocation(string Country, string City)
        {
            var geoCodeReq = new GeocodingRequest()
            {
                ApiKey = ApiConfig.ApiKey,
                Address = $"{Country} {City}"
            };
            var geoCodeEngine = GoogleMaps.Geocode;
            var geoCode = geoCodeEngine.QueryAsync(geoCodeReq);
            var location = geoCode.Result.Results.First().Geometry.Location;
            
            return (location.Latitude, location.Longitude);
        }
    }
    
}