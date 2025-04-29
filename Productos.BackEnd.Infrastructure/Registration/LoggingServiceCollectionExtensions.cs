using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Infrastructure.Registration
{
    /// <summary>
    /// Clase para poder añadir logging
    /// </summary>
    public static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services) 
        {
            services.AddLogging();

            return services;
        }
    }
}
