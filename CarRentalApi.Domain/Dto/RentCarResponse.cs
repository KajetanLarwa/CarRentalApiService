using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class RentCarResponse
    {
        [Required, Range(0, long.MaxValue)]
        [JsonPropertyName("priceId")]
        public long PriceId { get; set; }
        [Required, Range(0, long.MaxValue)]
        [JsonPropertyName("rentId")]
        public long RentId { get; set; }
        [Required, Range(0, long.MaxValue)]
        [JsonPropertyName("rentAt")]
        public DateTime RentAt { get; set; }
        [Required]
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [Required]
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
    }
}