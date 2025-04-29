using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Models
{
    /// <summary>
    /// Modelo para paginar los resultados de la consulta
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataPaginationModel<T> where T : class
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalSize { get; set; }

        public IEnumerable<T> Data { get; set;}
    }
}
