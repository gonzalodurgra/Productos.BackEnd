using Microsoft.EntityFrameworkCore;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Productos.BackEnd.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio base con el que interactúa el servicio base
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseAsyncRepository<TModel, TEntity>
    : IBaseAsyncRepository<TEntity>
    where TEntity : BaseEntity, new()
    where TModel : BaseModel, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public BaseAsyncRepository(DbContext context) 
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        /// <summary>
        /// Añade un elemento
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="audit"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(TEntity entity, AuditModel audit, bool save = true)
        {
           _dbSet.Add(entity);
            if (save)
            {
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        /// <summary>
        /// Borra un elemento
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="audit"></param>
        /// <param name="save"></param>
        /// <param name="logical"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity, AuditModel audit, bool save = true, bool logical = true)
        {
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
            if (save)
            {
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Recoge todos los elementos de una página determinada
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(DataPaginationModel<TEntity> pagination, bool enabled = true)
        {
            var query = _dbSet.AsQueryable();
            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * (pagination.PageSize)).Take(pagination.PageSize);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// Recoge un elemento por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<TEntity?> GetByIdAsync(int id, AuditModel audit, bool enabled = true)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        /// <summary>
        /// Actualiza un elemento
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="audit"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity, AuditModel audit, bool save = true)
        {
            _dbSet.Update(entity);
            if (save)
            {
                return await (_context.SaveChangesAsync());
            }
            return 0;
        }
    }
}
