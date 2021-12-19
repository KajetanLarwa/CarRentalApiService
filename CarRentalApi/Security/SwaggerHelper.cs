using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CarRentalApi.Security
{
    public static class SwaggerHelper
    {
        public static void AddSwaggerDetails(this IServiceCollection services)
        {
             services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "CarRentalApi", 
                        Description = "Car rental company api",
                        Contact = new OpenApiContact() { Name = "Dotnetrulez", Email = "maciej.b.lukasik@gmail.com" },
                        Version = "v1"
                    });
    
                    c.AddSecurityDefinition("x-api-key", new OpenApiSecurityScheme
                    {
                        Description = "Api key is required to access the endpoints.",
                        In = ParameterLocation.Header,
                        Name = "x-api-key",
                        Type = SecuritySchemeType.ApiKey
                    });
    
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { 
                            new OpenApiSecurityScheme 
                            {
                                Name = "x-api-key",
                                Type = SecuritySchemeType.ApiKey,
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                { 
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "x-api-key"
                                },
                            },
                            new string[] {}
                        }
                    });
                    
                    c.TagActionsBy(api =>
                    {
                        if (api.GroupName != null)
                        {
                            return new[] { api.GroupName };
                        }

                        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        {
                            return new[] { controllerActionDescriptor.ControllerName };
                        }

                        throw new InvalidOperationException("Unable to determine tag for endpoint.");
                    });
                    c.DocInclusionPredicate((name, api) => true);
                    
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    c.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
        }
    }
}