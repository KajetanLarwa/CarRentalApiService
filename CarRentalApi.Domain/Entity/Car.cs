using System;

namespace CarRentalApi.Domain.Entity
{
    public class Car
    {
        public int ID { get; set; }
        public int CarModelID { get; set; }
        public CarModel CarModel { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int ProductionYear { get; set; }
        public int Capacity { get; set; }
        public double BasePrice { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}