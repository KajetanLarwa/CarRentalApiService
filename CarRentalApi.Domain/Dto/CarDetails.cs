using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class CarDetails
    {
        [JsonPropertyName(("id"))]
        [Required, Range(0, long.MaxValue)]
        public long Id { get; set; }
        [Required]
        [JsonPropertyName(("brand"))]
        public string Brand { get; set; }
        [Required]
        [JsonPropertyName(("model"))]
        public string Model { get; set; }
        [Required]
        [JsonPropertyName(("productionYear"))]
        [Range(1900, 2100)]
        public int ProductionYear { get; set; }
        [Required]
        [JsonPropertyName(("capacity"))]
        [Range(0, 10)]
        public int Capacity { get; set; }
        [Required]
        [JsonPropertyName(("category"))]
        public string Category { get; set; }
        [Required]
        [JsonPropertyName("horsePower")]
        [Range(0, 1000)]
        public int HorsePower { get; set; }
        [Required]
        [JsonPropertyName("providerCompany")]
        public string ProviderCompany { get; set; }
    }
}