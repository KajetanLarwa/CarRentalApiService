using CarRentalApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Infrastructure.Database
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        { 
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Car");
        }
    }
    
}
