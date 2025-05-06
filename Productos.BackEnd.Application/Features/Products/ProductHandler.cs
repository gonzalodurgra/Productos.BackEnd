using AutoMapper;
using MediatR;
using Productos.BackEnd.Application.Features.Products.Commands;
using Productos.BackEnd.Application.Features.Products.Queries;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Features.Products
{
    /// <summary>
    /// Clase que trabaja con el repositorio de productos y los mapea
    /// </summary>
    public class ProductHandler : IRequestHandler<GetAllProductsQuery, DataPaginationModel<ProductResponseModel>>,
        IRequestHandler<GetProductsByIdQuery, ProductResponseModel>,
        IRequestHandler<CreateProductCommand, OkResponseModel>,
        IRequestHandler<UpdateProductCommand, OkResponseModel>,
        IRequestHandler<DeleteProductCommand, OkResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository<Product> _productRepository;

        public ProductHandler(IMapper mapper, IProductRepository<Product> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        /// <summary>
        /// Recoge todos los productos en una lista mapeada
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataPaginationModel<ProductResponseModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var pagination = new DataPaginationModel<Product>
            {
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };

            var products = await _productRepository.GetAllAsync(pagination);
            return _mapper.Map<DataPaginationModel<ProductResponseModel>>(products);
        }
        /// <summary>
        /// Recoge un solo producto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ProductResponseModel> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, new AuditModel());
            return _mapper.Map<ProductResponseModel>(product);
        }

        /// <summary>
        /// Crea un producto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OkResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product, new AuditModel());
            return new OkResponseModel
            {
                Id = product.Id,
                Message = "Producto insertado correctamente"
            };
        }

        /// <summary>
        /// Edita un producto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OkResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.UpdateAsync(product, new AuditModel());

            return new OkResponseModel
            {
                Id = product.Id,
                Message = "Producto actualizado correctamente"
            };
        }

        /// <summary>
        /// Borra un producto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OkResponseModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var audit = new AuditModel
            {
                
            };
            var product = await _productRepository.GetByIdAsync(request.Id, audit);
            await _productRepository.DeleteAsync(product, audit);
            return new OkResponseModel
            {
                Id = null,
                Message = "Producto borrado correctamente"
            };
        }
    }
}
