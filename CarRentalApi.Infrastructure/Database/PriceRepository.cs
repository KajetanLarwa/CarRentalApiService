using System;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.Out;

namespace CarRentalApi.Infrastructure.Database
{
    public class PriceRepository: IPriceRepository
    {
        private readonly CarRentalContext _context;

        public PriceRepository(CarRentalContext carRentalContext)
        {
            _context = carRentalContext;
        }
        
        public async Task<Price> GetPriceById(long priceId)
        {
            var price = await _context.Prices.FindAsync(priceId);
            return price;
        }
        
        public Price PutPrice(double priceValue, string currency, int daysCount, DateTime generatedAt, DateTime expiredAt)
        {
            var price = new Price() {Value = priceValue, Currency = currency, DaysCount = daysCount, GeneratedAt = generatedAt, ExpiredAt = expiredAt};
            _context.Prices.Add(price);
            _context.SaveChanges();
            return price;
        }
    }
}