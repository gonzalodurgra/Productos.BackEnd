using Swashbuckle.AspNetCore.Filters;
using Productos.BackEnd.Domain.Models;

namespace Productos.BackEnd.WebAPI.Controllers.Swagger
{
    /// <summary>
    /// Ejemplo de modelo de producto
    /// </summary>
    public class ProductModelExample : IExamplesProvider<ProductModel>
    {
        /// <summary>
        /// Devuelve un ejemplo
        /// </summary>
        /// <returns></returns>
        public ProductModel GetExamples()
        {
            return new ProductModel
            {
                Id = 1,
                Name = "Name",
                Price = 10,
                Stock = 5
            };
        }
    }
}
