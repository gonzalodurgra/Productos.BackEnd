using Productos.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Productos.BackEnd.WebAPI.Controllers.Swagger
{
    /// <summary>
    /// Ejemplo de una lista de productos
    /// </summary>
    public class ProductModelListExample : IExamplesProvider<IEnumerable<ProductModel>>
    {
        /// <summary>
        /// Devuelve el ejemplo de la lista
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetExamples()
        {
            return new List<ProductModel>
            {
                new ProductModelExample().GetExamples()
            };
        }
    }
}
