using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Productos.BackEnd.Application.Features.Products.Commands;
using Productos.BackEnd.Application.Features.Products.Queries;
using Productos.BackEnd.Business.Services;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq.Language.Flow;

namespace Productos.BackEnd.Business.Tests
{
    [TestClass]
    public class ProductServiceTest
    {
        private ProductService? _productService;
        private Mock<IMediator> _mediator;
        private Mock<IProductRepository<Product>> _productRepository;
        private Mock<IMapper> _mapper;

        #region Initialize&Cleanup
        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();
            _productService = new ProductService(_mediator!.Object, _productRepository!.Object, _mapper!.Object);
        }
        [TestCleanup]
        public void Cleanup()
        {
            _productService = null;
        }

        #endregion Initialize&Cleanup

        #region Mocks&Data

        private void CreateMocks()
        {
            _mediator = new Mock<IMediator>();
            _productRepository = new Mock<IProductRepository<Product>>();
            _mapper = new Mock<IMapper>();
        }

        private static DataPaginationModel<ProductModel> GetDataPaginationModel()
        {
            var productList = new List<ProductModel>
            {
                new ProductModel { Id = 1, Name = "Producto A", Price = 10.00m, Stock = 50 },
                new ProductModel { Id = 2, Name = "Producto B", Price = 20.00m, Stock = 30 },
                new ProductModel { Id = 3, Name = "Producto C", Price = 30.00m, Stock = 20 }
            };

            return new DataPaginationModel<ProductModel>
            {
                Data = productList,
                TotalSize = productList.Count,
                PageNumber = 1,
                PageSize = 10
            };
        }

        private static ProductResponseModel GetProductResponseModel()
        {
            return new ProductResponseModel
            {
                Id = 1,
                Name = "Name",
                Price = 15,
                Stock = 5
            };
        }

        private static ProductModel GetProductModel()
        {
            return new ProductModel
            {
                Id = 1,
                Name = "Name",
                Price = 15,
                Stock = 5
            };
        }

        private static Product GetProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "Name",
                Price = 15,
                Stock = 5
            };
        }

        private static AuditModel GetAuditModel()
        {
            return new AuditModel
            {
                Username = "GDG",
            };
        }

        private static OkResponseModel GetOkResponseModel()
        {
            return new OkResponseModel
            {
                Id = 1,
                Message = "Operación exitosa"
            };
        }

        #endregion Mocks&Data

        #region AddAsync
        [TestMethod]
        public async Task AddAsync_OK()
        {
            var expected = 1;
            var model = GetOkResponseModel();
            var product = GetProductModel();

            _mediator.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

            var result = await _productService.AddAsync(product, GetAuditModel());

            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(expected, result);
            Assert.IsNotNull(result);
        }
        #endregion AddAsync

        #region AddListAsync

        [TestMethod]
        public async Task AddAsyncList_OK()
        {
            var expected = new List<int>();
            expected.Add(1);
            var model = GetOkResponseModel();
            var products = new List<ProductModel>()
            {
                GetProductModel()
            };
            var results = new List<int>();
            

            foreach (var product in products)
            {
                _mediator!.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);
                results.Add(product.Id);
            }

            Assert.IsInstanceOfType(results, typeof(List<int>));
            foreach (var result in results)
            {
                Assert.AreEqual(1, result);
            }
            Assert.IsNotNull(results);
        }

        #endregion AddListAsync

        #region DeleteAsync

        [TestMethod]
        public async Task DeleteAsync_OK()
        {
            var model = GetOkResponseModel();

            _mediator!.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

            var result = await _productService.DeleteAsync(1, GetAuditModel());

            Assert.IsInstanceOfType(result, typeof(int));
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        #endregion DeleteAsync

        #region UpdateAsync
        [TestMethod]
        public async Task UpdateAsync_OK()
        {
            var expected = typeof(OkResponseModel);
            var model = GetOkResponseModel();
            var product = GetProductModel();
            _mapper!.Setup(m => m.Map<UpdateProductCommand>(It.IsAny<ProductModel>()))
                .Returns(new UpdateProductCommand(1, "nombre", 5, 1));

            _mediator!.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

            var result = await _productService!.UpdateAsync(1, product, GetAuditModel());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        #endregion UpdateAsync

        #region GetAllAsync

        [TestMethod]
        public async Task GetAllAsync_OK()
        {
            var expected = typeof(DataPaginationModel<ProductResponseModel>);

            var response = new DataPaginationModel<ProductResponseModel>
            {
                Data = new List<ProductResponseModel>
                {
                    new ProductResponseModel { Id = 1, Name = "Test Product", Price = 10.0m }
                },
                PageNumber = 1,
                PageSize = 1,
                TotalSize = 1
            };

            var mapped = new DataPaginationModel<ProductResponseModel>
            {
                Data = new List<ProductResponseModel>
                {
                    new ProductResponseModel { Id = 1, Name = "Test Product", Price = 10.0m }
                },
                PageNumber = 1,
                PageSize = 1,
                TotalSize = 1
            };

            _mediator!
                .Setup(x => x.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            _mapper!.Setup(m => m.Map<List<ProductResponseModel>>(response.Data))
                    .Returns(mapped.Data.ToList());

            var result = await _productService!.GetAllAsync(1, 1);

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [TestMethod]
        public async Task GetByIdAsync_OK()
        {
            var expected = typeof(ProductModel);
            var model = GetProductResponseModel();
            var product = GetProductModel();

            _mediator!.Setup(x => x.Send(It.IsAny<GetProductsByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(model);

            _mapper!.Setup(m => m.Map<ProductModel>(model))
                    .Returns(new ProductModel { Id = model.Id, Name = model.Name, Price = model.Price });

            var result = await _productService!.GetByIdAsync(1);

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        #endregion GetByIdAsync
    }
}
