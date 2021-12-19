using System;
namespace CarRentalApi.Domain.Entity
{
    public class Reservation
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
        public int PriceId { get; set; }
        public Price Price { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReturned { get; set; }
    }
}