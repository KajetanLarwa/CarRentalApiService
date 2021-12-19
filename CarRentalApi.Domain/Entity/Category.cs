using System.Collections.Generic;

namespace CarRentalApi.Domain.Entity
{
    public class Category
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}