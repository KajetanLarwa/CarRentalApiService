using System.Threading.Tasks;
using CarRentalApi.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalApi.Security
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!(context.Request.Path.Value?.StartsWith("/api") ?? false))
            {
                await _next(context);
                return;
            }
            
            if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new ApiErrorResponse() {Error = "Api Key was not provided"});
                return;
            }
            
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings[appSettings["ServiceApiKey"]];
            
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new ApiErrorResponse() { Error = "Incorrect api key" });
                return;
            }

            await _next(context);
        }
    }
}