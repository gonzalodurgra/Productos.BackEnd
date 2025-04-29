using Microsoft.AspNetCore.Mvc;
using Productos.BackEnd.Domain.Models;

namespace Productos.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Permite crear auditorías
    /// </summary>
    public static class ControllerUtility
    {
        /// <summary>
        /// Permite dar valores a la auditoría
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AuditModel GetAuditValues(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User?.Identity?.Name ?? "Anonymous";
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            return new AuditModel
            {
                Username = user,
                IpAddress = ipAddress,
                TimeStamp = DateTime.UtcNow,
            };
        }
    }
}
