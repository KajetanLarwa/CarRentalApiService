using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class CarPrice
    {
        [Required, Range(0, long.MaxValue)]
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [Required]
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [Required]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [Required]
        [JsonPropertyName("generatedAt")]
        public DateTime GeneratedAt { get; set; }
        [Required]
        [JsonPropertyName("expiredAt")]
        public DateTime ExpiredAt { get; set; }

    }
}