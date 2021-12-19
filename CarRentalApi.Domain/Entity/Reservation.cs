using System;
namespace CarRentalApi.Domain.Entity
{
    public class Reservation
    {
        public long ID { get; set; }
        public long CarID { get; set; }
        public Car Car { get; set; }
        public long PriceID { get; set; }
        public Price Price { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReturned { get; set; }
    }
}