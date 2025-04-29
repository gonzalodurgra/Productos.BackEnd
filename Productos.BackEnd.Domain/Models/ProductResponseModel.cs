using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Models
{
    /// <summary>
    /// Modelo que formateará la respuesta del servidor a la hora de realizar una consulta
    /// </summary>
    public class ProductResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
