using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Models
{
    /// <summary>
    /// Modelo para respuesta correcta del servidor
    /// </summary>
    public class OkResponseModel : BaseModel
    {
        public int? Id {  get; set; }

        public string Message { get; set; } = null!;
    }
}
