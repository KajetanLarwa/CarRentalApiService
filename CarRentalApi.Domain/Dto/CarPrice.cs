using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class CarPrice
    {
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}