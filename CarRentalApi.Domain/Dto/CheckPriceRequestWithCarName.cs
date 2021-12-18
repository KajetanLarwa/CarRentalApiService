using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class CheckPriceRequestWithCarName
    {
        [JsonPropertyName("brand")]
        [Required, MinLength(1)]
        public string Brand { get; set; }
        [JsonPropertyName("model")]
        [Required, MinLength(1)]
        public string Model { get; set; }
        [JsonPropertyName("yearsOfHavingLicense")]
        [Range(0, int.MaxValue)]
        public int YearsOfHavingLicense { get; set; }
        [JsonPropertyName("age")]
        [Range(0, int.MaxValue)]
        public int Age { get; set; }
        [JsonPropertyName("country")]
        [Required, MinLength(2)]
        public string Country { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("currentlyRentedCount")]
        [Range(0, int.MaxValue)]
        public int CurrentlyRentedCount { get; set; }
        [JsonPropertyName("overallRentedCount")]
        [Range(0, int.MaxValue)]
        public int OverallRentedCount { get; set; }
    }
}