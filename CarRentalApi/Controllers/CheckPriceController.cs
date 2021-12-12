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

        [HttpPost]
        public async Task<ActionResult<CarPrice>> CheckPrice([FromBody] CheckPriceRequest request)
        {
            var result = await _checkPriceUseCase.CheckPriceAsync(request);

            if (result.HasValue)
            {
                return Ok(new CarPrice()
                {
                    Price = result.Value,
                    Currency = "PLN"
                });
            }
            return NotFound("Cannot determine the price");
        }
        
    }
}