using System;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class CarPrice
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("available")]
        public bool Available { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("generatedAt")]
        public DateTime GeneratedAt { get; set; }
        [JsonPropertyName("expiredAt")]
        public DateTime ExpiredAt { get; set; }

    }
}