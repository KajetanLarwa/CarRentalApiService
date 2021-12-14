using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class RentCarRequest
    {
        [Range(0, int.MaxValue)]
        [JsonPropertyName("carId")]
        public int CarId { get; set; }
        [Required]
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [Required]
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        
    }
}