using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class ApiResponse
    {
        [Required]
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}