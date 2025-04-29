using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Contracts.Services
{
    /// <summary>
    /// Interfaz para el servicio base, con parámetro cualquier clase que herede de BaseModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseAsyncService<T> where T : BaseModel
    {
        Task<DataPaginationModel<T>> GetAllAsync(int pageNumber = 0, int pageSize = 0); 

        Task<T?> GetByIdAsync(int id);

        Task<int> AddAsync(T model, AuditModel audit);

        Task<IEnumerable<int>> AddAsync(IEnumerable<T> models, AuditModel audit);

        Task<int> UpdateAsync(int id, T model, AuditModel audit);

        Task<int> DeleteAsync(int id, AuditModel audit);
    }
}
