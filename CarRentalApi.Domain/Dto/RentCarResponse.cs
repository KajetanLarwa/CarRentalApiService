using System;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class RentCarResponse
    {
        [JsonPropertyName("priceId")]
        public int PriceId { get; set; }
        [JsonPropertyName("rentId")]
        public int RentId { get; set; }
        [JsonPropertyName("rentAt")]
        public DateTime RentAt { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        
    }
}