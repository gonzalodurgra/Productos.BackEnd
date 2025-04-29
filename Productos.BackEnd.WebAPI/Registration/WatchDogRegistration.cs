using System.Diagnostics.CodeAnalysis;
using WatchDog;
using WatchDog.src.Enums;

namespace Productos.BackEnd.WebAPI.Registration
{
    /// <summary>
    /// Añade y configura WatchDog en los servicios
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class WatchDogRegistration
    {
        /// <summary>
        /// Registra WatchDog
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWatchDogServices(this IServiceCollection services)
        {
               services.AddWatchDogServices(opt =>
               {
                   opt.IsAutoClear = Convert.ToBoolean(Application.Registration.ConfigurationManager.WatchDogAutoClear);
                   opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
               });

            return services;
        }
    }
}
