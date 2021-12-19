using System;
using System.Threading.Tasks;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain.Ports.Out
{
    public interface IPriceRepository
    {
        Price PutPrice(double priceValue, string currency, int daysCount, DateTime generatedAt, DateTime expiredAt);
        Task<Price> GetPriceById(long priceId);
    }
}