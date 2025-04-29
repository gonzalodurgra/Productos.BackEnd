using Microsoft.Extensions.DependencyInjection;
using Productos.BackEnd.Business.Services;
using Productos.BackEnd.Domain.Contracts.Services;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Business.Registration
{
    /// <summary>
    /// Registra la capa de negocio en los servicios
    /// </summary>
    public static class BusinessRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<AuditModel>();
            return services;
        }
    }
}
