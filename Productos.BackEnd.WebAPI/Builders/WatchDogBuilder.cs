using WatchDog;

namespace Productos.BackEnd.WebAPI.Builders
{
    /// <summary>
    /// Permite el uso de WatchDog
    /// </summary>
    public static class WatchDogBuilder
    {
        /// <summary>
        /// Configura WatchDog
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddWatchDogApp(this IApplicationBuilder app)
        {
            app.UseWatchDogExceptionLogger();
            app.UseWatchDog(opt =>
            {
                opt.WatchPageUsername = Application.Registration.ConfigurationManager.WatchDogUsername;
                opt.WatchPagePassword = Application.Registration.ConfigurationManager.WatchDogPassword;
            });

            return app;
        }
    }
}
