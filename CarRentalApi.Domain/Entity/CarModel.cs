using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Domain.Entity
{
    [Index(nameof(Brand), nameof(Model))]
    public class CarModel
    {
        public long ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public List<Car> Cars { get; set; }

        public CarModel(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }
    }
}