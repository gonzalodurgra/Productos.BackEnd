using Productos.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Productos.BackEnd.WebAPI.Controllers.Swagger
{
    /// <summary>
    /// Ejemplo de paginación
    /// </summary>
    public class ProductPaginationModelExample : IExamplesProvider<DataPaginationModel<ProductModel>>
    {
        /// <summary>
        /// Devuelve el ejemplo
        /// </summary>
        /// <returns></returns>
        public DataPaginationModel<ProductModel> GetExamples()
        {
            return new DataPaginationModel<ProductModel>
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new ProductModelListExample().GetExamples()
            };
        }
    }
}
