using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalApi.Security
{
    public class ApiKeyMiddleware
    {
        private const string API_KEY_NAME = "SecretApiKey";
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
                await context.Response.WriteAsync("Api Key was not provided");
                return;
            }
            
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings[appSettings[API_KEY_NAME]];
            
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"Unauthorized client");
                return;
            }

            await _next(context);
        }
    }
}