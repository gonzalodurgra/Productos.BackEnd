using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;


namespace Productos.BackEnd.WebAPI.Registration
{
    /// <summary>
    /// Registra Swagger
    /// </summary>
    public static class SwaggerRegistration
    {
        /// <summary>
        /// Añade y configura Swagger en los servicios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            var enabled = Convert.ToBoolean(Application.Registration.ConfigurationManager.SwaggerEnabled);

            if (enabled)
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = Application.Registration.ConfigurationManager.WebAPIName,
                        Description = "API para manejar productos"
                    });

                    options.AddSecurityDefinition("Basic", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Description = "Autorización JWT para la API",
                        Name = "Autorización",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                        Scheme = "Basic"
                    });

                    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Basic"
                                }
                            },
                            new string[]{}
                        }
                    });

                    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
                    options.ExampleFilters();

                });

                services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
                services.AddFluentValidationRulesToSwagger();
            }
            return services;
        }
    }
}
