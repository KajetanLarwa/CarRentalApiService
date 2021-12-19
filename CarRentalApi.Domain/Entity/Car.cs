using System;
using CarRentalApi.Domain.Dto;

namespace CarRentalApi.Domain.Entity
{
    public class Car
    {
        public long ID { get; set; }
        public long CarModelID { get; set; }
        public CarModel CarModel { get; set; }
        public long CategoryID { get; set; }
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