using System;
using CarRentalApi.Domain.Do;

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
        public int HorsePower { get; set; }
        public string ProviderCompany { get; set; }

        public CarDetails ToDto()
        {
            return new CarDetails()
            {
                Id = ID,
                Brand = CarModel.Brand,
                Model = CarModel.Model,
                ProductionYear = ProductionYear,
                Capacity = Capacity,
                Category = Category.Name,
                HorsePower = HorsePower,
                ProviderCompany = ProviderCompany
            };
        }
    }
}