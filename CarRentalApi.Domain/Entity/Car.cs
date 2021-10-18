using System;

namespace CarRentalApi.Domain.Entity
{
    public class Car
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ProductionDate{ get; set; }
        public string Type { get; set; }
    }
}