using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Contracts.Services
{
    /// <summary>
    /// Interfaz de la que hereda el servicio de Productos, que a su vez hereda de la interfaz base con el modelo de productos
    /// </summary>
    public interface IProductService : IBaseAsyncService<ProductModel>
    {

    }
}
