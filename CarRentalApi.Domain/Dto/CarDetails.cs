using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Do
{
    public class CarDetails
    {
        [JsonPropertyName(("id"))]
        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        [JsonPropertyName(("brand"))]
        [Required]
        public string Brand { get; set; }
        [JsonPropertyName(("model"))]
        [Required]
        public string Model { get; set; }
        [JsonPropertyName(("productionYear"))]
        [Range(1900, 2100)]
        public int ProductionYear { get; set; }
        [JsonPropertyName(("capacity"))]
        [Range(0, 10)]
        public int Capacity { get; set; }
        [JsonPropertyName(("category"))]
        [Required]
        public string Category { get; set; }
        [JsonPropertyName("horsePower")]
        [Range(0, 1000)]
        public int HorsePower { get; set; }
        [JsonPropertyName("providerCompany")]
        [Required]
        public string ProviderCompany { get; set; }
    }
}