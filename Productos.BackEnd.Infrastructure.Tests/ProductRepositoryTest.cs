using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using Productos.BackEnd.Infrastructure.Context;
using Productos.BackEnd.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Infrastructure.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private ProductRepository? _productRepository;
        private ProductDBContext? _context;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateContext();

            _productRepository = new ProductRepository(_context!);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _productRepository = null;
            _context?.Dispose();
        }

        #endregion Initialize&Cleanup

        #region Mocks&Data

        private void CreateContext()
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase(databaseName: $"ProductDbContext-{Guid.NewGuid()}").Options;

            _context = new ProductDBContext(options);

            _context.Products!.AddRange(GetProducts());

            _context.SaveChanges();

        }

        private Product GetProduct()
        {
            return new Product
            {
                Id = 5,
                Name = "Test5",
                Price = 1,
                Stock = 1
            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                { 
                    Id = 1,
                    Name = "Test1",
                    Price = 1,
                    Stock = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Test2",
                    Price = 2,
                    Stock = 3
                },
                new Product
                {
                    Id = 3,
                    Name = "Test3",
                    Price = 3,
                    Stock = 4
                }
            };
        }

        #endregion Mocks&Data

        #region Add

        [TestMethod]
        public async Task Add_OK()
        {
            var expected = typeof(int);

            var result = await _productRepository.AddAsync(GetProduct());

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        #endregion Add

        #region Delete

        [TestMethod]
        public async Task Delete_OK()
        {
            var entity = GetProduct();

            _context.Products.Add(entity);

            await _productRepository.DeleteAsync(entity);

            var deleted = await _context.Products.FindAsync(entity.Id);
            Assert.IsNull(deleted);
        }

        #endregion Delete

        #region Update

        public async Task Update_OK()
        {
            var expected = typeof (OkResponseModel);
            var result = await _productRepository.UpdateAsync(GetProduct());
            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion Update

        #region GetAll

        [TestMethod]
        public async Task GetAll_OK()
        {
            var expected = typeof(IEnumerable<Product>);

            var result = await _productRepository!.GetAllAsync(new Domain.Models.DataPaginationModel<Product>
            {
                PageNumber = 1,
                PageSize = 1
            });

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        #endregion GetAll

        #region GetById

        [TestMethod]
        public async Task GetById_OK()
        {
            var expected = typeof(Product);

            var result = await _productRepository!.GetByIdAsync(1);

            Assert.IsInstanceOfType(result, expected);
            Assert.AreEqual(1, result.Id);
        }

        #endregion GetById
    }
}
