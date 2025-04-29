using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Models
{
    /// <summary>
    /// Modelo para auditoría
    /// </summary>
    public class AuditModel
    {
        public string Username { get; set; }

        public string IpAddress { get; set; }

        public string Action { get; set; }

        public string EntityName { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
