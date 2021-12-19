using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class CheckPriceRequest
    {
        [JsonPropertyName("yearsOfHavingLicense")]
        [Required, Range(0, int.MaxValue)]
        public int YearsOfHavingLicense { get; set; }
        [JsonPropertyName("age")]
        [Required, Range(0, int.MaxValue)]
        public int Age { get; set; }
        [JsonPropertyName("country")]
        [Required, MinLength(2)]
        public string Country { get; set; }
        [JsonPropertyName("city")]
        [Required]
        public string City { get; set; }
        [JsonPropertyName("currentlyRentedCount")]
        [Required, Range(0, int.MaxValue)]
        public int CurrentlyRentedCount { get; set; }
        [JsonPropertyName("overallRentedCount")]
        [Required, Range(0, int.MaxValue)]
        public int OverallRentedCount { get; set; }
        [JsonPropertyName("daysCount")]
        [Required, Range(1, int.MaxValue)]
        public int DaysCount { get; set; }
    }
}