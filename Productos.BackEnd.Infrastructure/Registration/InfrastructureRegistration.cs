using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Infrastructure.Context;
using Productos.BackEnd.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Infrastructure.Registration
{
    /// <summary>
    /// Clase que registra el servicio de infraestructura, añadiendo aquí el contexto de la base de datos, repositorios y logs.
    /// </summary>
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDBContext>(options =>
            {
                options.UseSqlServer(Application.Registration.ConfigurationManager.Configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IProductRepository<Product>, ProductRepository>();

            services.AddLoggingServices();

            return services;
        }
    }
}
