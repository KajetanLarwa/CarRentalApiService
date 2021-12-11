using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Dto
{
    public class ReturnCarRequest
    {
        [Range(0, int.MaxValue)]
        [JsonPropertyName("reservationId")]
        public long ReservationId { get; set; }
    }
}