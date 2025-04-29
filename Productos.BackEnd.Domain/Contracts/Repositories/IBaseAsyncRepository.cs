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
    /// Interfaz para crear repositorios que interactúan con una determinada entidad
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseAsyncRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(DataPaginationModel<T> pagination, bool enabled = true);

        Task<T?> GetByIdAsync(int id, AuditModel audit, bool enabled = true);

        Task<int> AddAsync(T entity, AuditModel audit, bool save = true);

        Task<int> UpdateAsync(T entity, AuditModel audit, bool save = true);

        Task DeleteAsync(T entity, AuditModel audit, bool save = true, bool logical = true);

    }
}
