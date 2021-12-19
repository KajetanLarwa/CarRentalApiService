using System;
using System.Globalization;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Services;
using Xunit;

namespace CarRentalApi.Domain.Test.Services
{
    public class PriceCalculatorTest
    {
        [Fact]
        public void Task_Correct_Price()
        {
            var calculator = new PriceCalculator();
            
            var car = new Car() {BasePrice = 20, Latitude = 45.22, Longitude = 46.22};
            var years = 5;
            var age = 89;
            var latitude = 67;
            var longitude = 23;
            var currentlyRented = 78;
            var overallRented = 4;
            var price = calculator.Calculate(car, years, age, latitude, longitude, currentlyRented, overallRented, 3);
            
            Assert.True(price >= 0);
        }
        
        [Fact]
        public void Task_Negative_Price()
        {
            var calculator = new PriceCalculator();
            
            var car = new Car() {BasePrice = -20, Latitude = -45.22, Longitude = -46.22};
            var years = -5;
            var age = -89;
            var latitude = -67;
            var longitude = -23;
            var currentlyRented = -78;
            var overallRented = -4;
            var price = calculator.Calculate(car, years, age, latitude, longitude, currentlyRented, overallRented, 3);
            
            Assert.True(price >= 0);
        }
    }
}