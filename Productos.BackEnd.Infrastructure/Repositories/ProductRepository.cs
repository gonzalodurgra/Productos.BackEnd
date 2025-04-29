using Microsoft.EntityFrameworkCore;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using Productos.BackEnd.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para los productos, hereda del repositorio base e implementa su interfaz de productos, trabajando con entidades
    /// </summary>
    public class ProductRepository : BaseAsyncRepository<ProductModel, Product>, IProductRepository<Product>
    {
        private readonly ProductDBContext _context;
        public ProductRepository(ProductDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Añade un producto
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(Product entity, bool save = true)
        {
            _context.Products.Add(entity);
            if (save)
            {
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        /// <summary>
        /// Borra un producto
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <param name="logical"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Product entity, bool save = true, bool logical = true)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Recoge productos paginados
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        new public async Task<IEnumerable<Product>> GetAllAsync(DataPaginationModel<Product> pagination, bool enabled = true)
        {
            var query = _context.Products.AsQueryable();
            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * (pagination.PageSize)).Take(pagination.PageSize);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// Recoge un producto por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<Product?> GetByIdAsync(int id, bool enabled = true)
        {
            var producto = await _context.Products.FindAsync(id);
            return producto;
        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Product entity, bool save = true)
        {
            _context.Products.Update(entity);
            if(save)
            {
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
