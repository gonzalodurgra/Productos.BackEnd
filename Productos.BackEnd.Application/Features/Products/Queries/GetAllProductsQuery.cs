using MediatR;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Features.Products.Queries
{
    /// <summary>
    /// Instrucción para recibir todos los productos de una página determinada por GET
    /// </summary>
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponseModel>>
    {
        public int DataSourceId { get; set; }

        public DataPaginationModel<ProductModel> Pagination { get; set; }
    }
}
