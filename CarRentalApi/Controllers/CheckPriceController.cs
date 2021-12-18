using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalApi.Domain;
using CarRentalApi.Domain.Dto;
using CarRentalApi.Domain.Entity;
using CarRentalApi.Domain.Ports.In;
using CarRentalApi.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPriceController : ControllerBase
    {
        private readonly ICheckPriceUseCase _checkPriceUseCase;

        public CheckPriceController(ICheckPriceUseCase checkPriceUseCase)
        {
            _checkPriceUseCase = checkPriceUseCase;
        }

        /// <summary>
        /// Return calculated price of renting the car by car ID
        /// </summary>
        /// <response code="200">Successfully calculated price</response>
        /// <response code="409">Cannot calculate the price</response>
        [HttpPost]
        public async Task<ActionResult<CarPrice>> CheckPrice([FromBody] CheckPriceRequestWithCarId request)
        {
            var result = await _checkPriceUseCase.CheckPriceAsync(request);

            if (result.HasValue)
            {
                var price = _checkPriceUseCase.SaveCalculatedPrice(result.Value, "PLN");
                return Ok(new CarPrice()
                {
                    ID = price.ID,
                    Available = true,
                    Price = price.Value,
                    Currency = price.Currency,
                    GeneratedAt = price.GeneratedAt,
                    ExpiredAt = price.ExpiredAt
                });
            }
            return Conflict(new CarPrice()
            {
                Available = true,
                Error = "Cannot determine the price",
                Price = null,
            });
        }
        
        /// <summary>
        /// Return calculated price of renting the car by car model and brand
        /// </summary>
        /// <response code="200">Successfully calculated price</response>
        /// <response code="409">Cannot calculate the price</response>
        [HttpPost]
        public async Task<ActionResult<CarPrice>> CheckPrice([FromBody] CheckPriceRequestWithCarName request)
        {
            var result = await _checkPriceUseCase.CheckPriceAsync(request);

            if (result.HasValue)
            {
                return Ok(new CarPrice()
                {
                    Available = true,
                    Price = result.Value,
                    Currency = "PLN"
                });
            }
            return Conflict(new CarPrice()
            {
                Available = true,
                Error = "Cannot determine the price",
                Price = null,
            });
        }
        
    }
}