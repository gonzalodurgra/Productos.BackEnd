using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Registration
{
    /// <summary>
    /// Clase que recoge la configuración de la aplicación en appsettings.json
    /// </summary>
    public class ConfigurationManager
    {
        public static IConfiguration? Configuration { get; set; }

        #region Swagger
        /// <summary>
        /// Swagger activo
        /// </summary>
        public static bool SwaggerEnabled
        {
            get
            {
                if (Configuration != null && bool.TryParse(Configuration["Swagger:Enabled"], out bool enabled))
                {
                    return enabled;
                }
                return false;
            }
        }

        /// <summary>
        /// Nombre de la API
        /// </summary>
        public static string WebAPIName
        {
            get
            {
                return Configuration != null ? Configuration["Swagger:api"] : string.Empty;
            }
        }
        #endregion Swagger

        #region ConnectionStrings

        /// <summary>
        /// Conexión con la base de datos SQL Server
        /// </summary>
        public static string ProductsDB
        {
            get
            {
                return Configuration != null ? Configuration["ConnectionStrings:Default"] : string.Empty;
            }
        }
        #endregion ConnectionStrings

        #region WatchDog

        /// <summary>
        /// Usuario de WatchDog
        /// </summary>
        public static string WatchDogUsername
        {
            get
            {
                return Configuration != null ? Configuration["WatchDog:username"] : string.Empty;
            }
        }

        /// <summary>
        /// Contraseña de WatchDog
        /// </summary>
        public static string WatchDogPassword
        {
            get
            {
                return Configuration != null ? Configuration["WatchDog:password"] : string.Empty;
            }
        }

        /// <summary>
        /// Controla la eliminación de los logs cada cierto tiempo, aunque aún no se ha configurado el tiempo
        /// </summary>
        public static bool WatchDogAutoClear
        {
            get
            {
                if (Configuration != null && bool.TryParse(Configuration["WatchDog:AutoClear"], out bool autoClear))
                {
                    return autoClear;
                }
                return false;
            }
        }

        #endregion WatchDog
    }


}
