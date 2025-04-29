using AutoMapper;
using MediatR;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Domain.Contracts.Services
{
    /// <summary>
    /// Servicio base del que hereda ProductRepository (el patrón mediator lo usamos en ProductRepository), adaptable a cualquier modelo para el servicio y entidad 
    /// para el repositorio
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="E"></typeparam>
    public class BaseAsyncService<T, E> : IBaseAsyncService<T> where T : BaseModel, new() where E : BaseEntity , new()
    {
        protected IMediator mediator;
        protected readonly IBaseAsyncRepository<E> _repository;
        protected readonly IMapper _mapper;

        public BaseAsyncService(IMediator mediator, IBaseAsyncRepository<E> repository, IMapper mapper)
        {
            this.mediator = mediator;
            this._repository = repository;
            this._mapper = mapper;
        }
        /// <summary>
        /// Añade al repositorio un elemento
        /// </summary>
        /// <param name="model"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T model, AuditModel audit)
        {
            var entity = _mapper.Map<E>(model);
            return await _repository.AddAsync(entity, audit);
        }
        /// <summary>
        /// Añade al repositorio una lista
        /// </summary>
        /// <param name="models"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> AddAsync(IEnumerable<T> models, AuditModel audit)
        {
            var ids = new List<int>();
            foreach (var model in models)
            {
                var entity = _mapper.Map<E>(model);
                var id = await _repository.AddAsync(entity, audit);
                ids.Add(id);
            }
            return ids;
        }
        /// <summary>
        /// Borra del repositorio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> DeleteAsync(int id, AuditModel audit)
        {
            var entity = await _repository.GetByIdAsync(id, audit);
            if(entity == null)
            {
                throw new Exception("No encontrado");
            }
            await _repository.DeleteAsync(entity, audit);
            return id;
        }
        /// <summary>
        /// Recoge una lista de elementos paginados
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<DataPaginationModel<T>> GetAllAsync(int pageNumber = 0, int pageSize = 0)
        {
            var pagination = new DataPaginationModel<E>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var entities = await _repository.GetAllAsync(pagination);
            var models = _mapper.Map<List<T>>(entities);

            return new DataPaginationModel<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = models.Count,
                Data = models
            };
        }
        /// <summary>
        /// Recoge un elemento por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id, new AuditModel());
            if (entity == null) return null;

            return _mapper.Map<T>(entity);
        }

        /// <summary>
        /// Actualiza un elemento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> UpdateAsync(int id, T model, AuditModel audit)
        {
            var entity = await _repository.GetByIdAsync(id, audit);
            if (entity == null)
            {
                throw new Exception("No encontrado");
            }

            var updatedEntity = _mapper.Map<E>(model);
            updatedEntity.Id = id;

            return await _repository.UpdateAsync(updatedEntity, audit);
        }

  
    }
}
