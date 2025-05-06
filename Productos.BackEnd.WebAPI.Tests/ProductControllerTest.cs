using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Productos.BackEnd.Domain.Contracts.Services;
using Productos.BackEnd.Domain.Models;
using Productos.BackEnd.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.WebAPI.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        private ProductController? _productController;
        private Mock<IProductService> _productService;

        #region Initialize&Cleanup
        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();
            _productController = new ProductController(_productService!.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _productController = null;
        }
        #endregion Initialize&Cleanup

        #region Mocks&Data

        public void CreateMocks()
        {
            _productService = new Mock<IProductService>();
        }
        private static DataPaginationModel<ProductModel> GetDataPaginationModel()
        {
            var productList = new List<ProductModel>()
            {
                GetProductModel()
            };

            return new DataPaginationModel<ProductModel>
            {
                Data = productList,
                TotalSize = productList.Count,
                PageNumber = 1,
                PageSize = 10
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

        private static AuditModel GetAuditModel()
        {
            return new AuditModel
            {
                Username = "Anonymous"
            };
        }
        #endregion Mocks&Data

        #region CreateProduct

        [TestMethod]
        public async Task CreateProduct_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetProductModel();

            _productController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    Connection = { RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1") },
                    User = new System.Security.Claims.ClaimsPrincipal(
                    new System.Security.Claims.ClaimsIdentity(
                        new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "TestUser") }
                        )
                    )
                }
            };

            _productService!.Setup(x => x.AddAsync(It.IsAny<ProductModel>(), It.IsAny<AuditModel>())).ReturnsAsync(1);

            IActionResult result = await _productController!.CreateProduct(model);
            var data = (result as OkObjectResult)?.Value as int?;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data);
        }

        #endregion CreateProduct

        #region UpdateProduct

        [TestMethod]
        public async Task UpdateProduct_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetProductModel();

            _productService!.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductModel>(), It.IsAny<AuditModel>())).ReturnsAsync(1);

            IActionResult result = await _productController.UpdateProduct(1, model, GetAuditModel());
            var data = (result as OkObjectResult)?.Value as int?;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data);
        } 

        #endregion UpdateProduct

        #region DeleteProduct

        public async Task DeleteProduct_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetProductModel();

            _productService!.Setup(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<AuditModel>())).ReturnsAsync(1);

            IActionResult result = await _productController.DeleteProduct(1, GetAuditModel());
            var data = (result as OkObjectResult)?.Value as int?;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data);

        }

        #endregion DeleteProduct

        #region GetAllProducts

        [TestMethod]
        public async Task GetAllProducts_OK()
        {
            var expected = typeof(OkObjectResult);
            var expectedList = new List<ProductModel>
            {
                new ProductModel
                {
                    Id = 1,
                    Name = "Producto prueba",
                    Price = 100
                }
            };
            var model = GetDataPaginationModel();



            _productService!.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new DataPaginationModel<ProductModel>
            {
                Data = expectedList,
                TotalSize = expectedList.Count()
            });

            IActionResult result = await _productController!.GetAllProducts(1, 10);

            var data = (result as OkObjectResult)?.Value as List<ProductModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data!.Count());
        }

        #endregion GetAllProducts

        #region GetProductById

        [TestMethod]
        public async Task GetProductById_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetProductModel();

            _productService!.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

            IActionResult result = await _productController!.GetProductById(1);
            var data = (result as OkObjectResult)?.Value as ProductModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Id);
        }

        #endregion GetProductById
    }
}
