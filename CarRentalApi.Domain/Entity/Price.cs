using System;
using System.Text.Json.Serialization;

namespace CarRentalApi.Domain.Entity
{
    public class Price
    {
        public int ID { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}