using System;
using System.Collections.Generic;
using System.ComponentModel;
using CarRentalApi.Domain.Entity;

namespace CarRentalApi.Domain
{
    public class PriceCalculator
    {
        private const double DefaultFactor = 0.0;
        
        public double Calculate(Car car, int yearsOfHavingLicense, int age, double latitude, double longitude,
            int currentlyRentedCount, int overallRentedCount)
        {
            var price = car.BasePrice * (1 + LicenseFactor(yearsOfHavingLicense) + AgeFactor(age) +
                                                LocationFactor(car.Latitude, car.Longitude, latitude, 
                                                    longitude) + CurrentlyRentedFactor(currentlyRentedCount) +
                                                OverallRentedFactor(overallRentedCount));
            return Math.Max(0, price);
        }

        private double LicenseFactor(int years)
        {
            const double factorIncrement = 0.1;
            return years switch
            {
                < 1 => DefaultFactor + 2 * factorIncrement,
                < 2 => DefaultFactor + factorIncrement,
                _ => DefaultFactor
            };
        }

        private double AgeFactor(int age)
        {
            const double factorIncrement = 0.1;
            return age switch
            {
                < 21 => DefaultFactor + factorIncrement,
                _ => DefaultFactor
            };
        }

        private double LocationFactor(double carLatitude, double carLongitude, double clientLatitude, 
            double clientLongitude)
        {
            const double factorIncrement = 0.15;
            var distance = Math.Sqrt(Math.Pow(carLatitude - clientLatitude, 2) +
                Math.Pow(carLongitude - clientLongitude, 2));
            return distance switch
            {
                > 30 => DefaultFactor + 5 * factorIncrement,
                > 13 => DefaultFactor + 3 * factorIncrement,
                > 8 => DefaultFactor + 2 * factorIncrement,
                > 3 => DefaultFactor + factorIncrement,
                _ => DefaultFactor
            };
        }

        private double CurrentlyRentedFactor(int rentedCount)
        {
            const double factorIncrement = -0.1;
            return rentedCount switch
            {
                < 5 => DefaultFactor,
                < 15 => DefaultFactor + factorIncrement,
                _ => DefaultFactor + 2 * factorIncrement
            };
        }
        
        private double OverallRentedFactor(int rentedCount)
        {
            const double factorIncrement = -0.12;
            return rentedCount switch
            {
                < 15 => DefaultFactor,
                < 25 => DefaultFactor + factorIncrement,
                _ => DefaultFactor + 2 * factorIncrement
            };
        }
    }
}