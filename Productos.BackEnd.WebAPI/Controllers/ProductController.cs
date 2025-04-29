using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Productos.BackEnd.Domain.Contracts.Services;
using Productos.BackEnd.Domain.Models;
using Productos.BackEnd.WebAPI.Controllers.Swagger;
using Swashbuckle.AspNetCore.Filters;

namespace Productos.BackEnd.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de gestionar productos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        /// <summary>
        /// Constructor del controlador
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService )
        {
            this._productService = productService;
        }

        /// <summary>
        /// Crea un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(OkResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
        {
            var audit = ControllerUtility.GetAuditValues(ControllerContext);
            return Ok(await _productService.AddAsync(product, audit));
        }
        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OkResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductModel product, [FromServices] AuditModel audit)
        {
            var updatedId = await _productService.UpdateAsync(id, product, audit);
            return Ok(updatedId);
        }
        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OkResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteProduct(int id, [FromServices] AuditModel audit)
        {
            await _productService.DeleteAsync(id, audit);
            return NoContent();
        }

        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        /// <param name="pageNumber">Página a obtener</param>
        /// <param name="pageSize">Tamaño de la página</param>
        /// <returns>Lista de productos</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductModelListExample))]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 0)
        {
            var result = await _productService.GetAllAsync(pageNumber, pageSize);
            return Ok(result.Data);
        }
        /// <summary>
        /// Obtiene un solo producto por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
