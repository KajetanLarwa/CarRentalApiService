using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class ApiResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        
        public ApiResponse(string message)
        {
            Message = message;
        }
    }
}