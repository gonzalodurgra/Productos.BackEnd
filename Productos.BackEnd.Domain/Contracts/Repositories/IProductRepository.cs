using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Contracts.Repositories
{
    /// <summary>
    /// Inetfaz del repositorio que interactúa con la entidad de productos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProductRepository<T> : IBaseAsyncRepository<T> where T : Product
    {

    }
}
