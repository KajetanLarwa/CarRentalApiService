using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class ApiErrorResponse
    {
        [Required]
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}