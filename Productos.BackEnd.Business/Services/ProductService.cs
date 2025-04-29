using MediatR;
using AutoMapper;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Contracts.Services;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productos.BackEnd.Application.Features.Products.Queries;
using Productos.BackEnd.Application.Features.Products.Commands;

namespace Productos.BackEnd.Business.Services
{
    /// <summary>
    /// Clase para el servicio de productos (No se usa aún los parámetros de auditoría), trabajando con AutoMapper y Mediator
    /// </summary>
    public class ProductService : BaseAsyncService<ProductModel, Product>, IProductService
    {
        private readonly IMediator _mediator;
        new private readonly IProductRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IProductRepository<Product> productRepository, IMapper mapper) : base (mediator, productRepository, mapper)
        {
            _mediator = mediator;
            _repository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Agrega un solo producto
        /// </summary>
        /// <param name="model"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        new public async Task<int> AddAsync(ProductModel model, AuditModel audit)
        {
            var command = _mapper.Map<CreateProductCommand>(model);
            var result = await _mediator.Send(command);
            return result.Id ?? 0;
        }

        /// <summary>
        /// Agrega una lista de productos
        /// </summary>
        /// <param name="models"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        new public async Task<IEnumerable<int>> AddAsync(IEnumerable<ProductModel> models, AuditModel audit)
        {
            var results = new List<int>();

            foreach (var model in models)
            {
                var command = _mapper.Map<CreateProductCommand>(model);
                var result = await _mediator.Send(command);
                if (result.Id.HasValue)
                {
                    results.Add(result.Id.Value);
                }
            }
            return results;
        }

        /// <summary>
        /// Borra un producto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        new public async Task<int> DeleteAsync(int id, AuditModel audit)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return id;
        }

        /// <summary>
        /// Permite consultar productos con paginación
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        new public async Task<DataPaginationModel<ProductModel>> GetAllAsync(int pageNumber = 0, int pageSize = 0)
        {
            var query = new GetAllProductsQuery
            {
                Pagination = new DataPaginationModel<ProductModel>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            };

            var response = await _mediator.Send(query);
            return new DataPaginationModel<ProductModel>
            {
                TotalSize = response.Count(),
                Data = _mapper.Map<List<ProductModel>>(response)
            };


        }

        /// <summary>
        /// Recoge un producto por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new public async Task<ProductModel?> GetByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetProductsByIdQuery(id));
            return _mapper.Map<ProductModel?>(result);
        }

        new public async Task<int> UpdateAsync(int id, ProductModel model, AuditModel audit)
        {
            var command = _mapper.Map<UpdateProductCommand>(model);
            command.Id = id;
            var result = await _mediator.Send(command);
            return result.Id ?? 0;
        }
    }
}
