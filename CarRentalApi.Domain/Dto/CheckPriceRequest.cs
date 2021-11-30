using System;
using System.ComponentModel.DataAnnotations;
using CarRentalApi.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Domain
{
    public class CheckPriceRequest
    {
        [Range(0, int.MaxValue)]
        public int CarId { get; set; }
        [Range(0, int.MaxValue)]
        public int YearsOfHavingLicense { get; set; }
        [Range(0, int.MaxValue)]
        public int Age { get; set; }
        [Required, MinLength(2)]
        public string Country { get; set; }
        public string City { get; set; }
        [Range(0, int.MaxValue)]
        public int CurrentlyRentedCount { get; set; }
        [Range(0, int.MaxValue)]
        public int OverallRentedCount { get; set; }
    }
}