using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Productos.BackEnd.Application.Features.Products;
using Productos.BackEnd.Application.Features.Products.Commands;
using Productos.BackEnd.Application.Features.Products.Queries;
using Productos.BackEnd.Application.Mappings;
using Productos.BackEnd.Domain.Contracts.Repositories;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Tests
{
    [TestClass]
    public class ProductHandlerTest
    {
        private ProductHandler? _productHandler;
        private IMapper? _mapper;
        private Mock<IProductRepository<Product>> _productRepository;


        #region Initialize&Cleanup
        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();
            CreateMapping();

            _productHandler = new ProductHandler(_mapper!, _productRepository!.Object);
        }
        [TestCleanup]
        public void Cleanup()
        {
            _productHandler = null;
            _mapper = null;
        }

        #endregion Initialize&Cleanup

        #region Mocks&Data

        private void CreateMocks()
        {
            _productRepository = new Mock<IProductRepository<Product>>();
        }

        private void CreateMapping()
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new ProductProfile()));
            _mapper = mapperConfig.CreateMapper();
        }

        public Product GetProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "Name",
                Price = 15,
                Stock = 5
            };
        }

        #endregion Mocks&Data

        #region GetAll

        [TestMethod]
        public async Task GetAll_OK()
        {
            var expected = typeof(DataPaginationModel<ProductResponseModel>);
            var entities = new List<Product>()
            {
                GetProduct()
            };

            var request = new GetAllProductsQuery(1, new DataPaginationModel<Product>());

            _productRepository!.Setup(x => x.GetAllAsync(It.IsAny<DataPaginationModel<Product>>(), It.IsAny<bool>())).ReturnsAsync(entities);

            var result = await _productHandler!.Handle(request, new CancellationToken());

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Data.Count());
        }

        #endregion GetAll

        #region GetById

        [TestMethod]
        public async Task GetById_OK()
        {
            var expected = typeof(ProductResponseModel);
            var entity = GetProduct();

            var request = new GetProductsByIdQuery(1);

            _productRepository!.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<AuditModel>(), It.IsAny<bool>())).ReturnsAsync(entity);

            var result = await _productHandler!.Handle(request, new CancellationToken());

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        #endregion GetById

        #region Create

        [TestMethod]
        public async Task Create_OK()
        {
            var expected = typeof(OkResponseModel);
            var entity = GetProduct();

            var request = new CreateProductCommand(entity.Id, entity.Name, entity.Price, entity.Stock);

            _productRepository!.Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<AuditModel>(), It.IsAny<bool>())).ReturnsAsync(1);

            var result = await _productHandler!.Handle(request, new CancellationToken());

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        #endregion Create

        #region Update

        [TestMethod]
        public async Task Update_OK()
        {
            var expected = typeof(OkResponseModel);
            var entity = GetProduct();

            var request = new UpdateProductCommand(entity.Id, entity.Name, entity.Price, entity.Stock);

            _productRepository!.Setup(x => x.UpdateAsync(It.IsAny<Product>(), It.IsAny<AuditModel>(), It.IsAny<bool>())).ReturnsAsync(1);

            var result = await _productHandler!.Handle(request, new CancellationToken());

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        #endregion Update

        #region Delete

        [TestMethod]
        public async Task Delete_OK()
        {
            var expected = typeof (OkResponseModel);
            var entity = GetProduct();

            var request = new DeleteProductCommand(entity.Id);

            _productRepository!.Setup(x => x.DeleteAsync(It.IsAny<Product>(), It.IsAny<AuditModel>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(Task.CompletedTask);

            var result = await _productHandler!.Handle(request, new CancellationToken());

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
        }

        #endregion Delete
    }
}
