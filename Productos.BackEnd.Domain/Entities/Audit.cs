using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Entities
{
    /// <summary>
    /// Clase para auditoría
    /// </summary>
    public class Audit
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string IpAddress { get; set; }

        public string Action { get; set; }

        public string EntityName { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
