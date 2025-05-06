using MediatR;
using Productos.BackEnd.Domain.Entities;
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
    public class GetAllProductsQuery : IRequest<DataPaginationModel<ProductResponseModel>>
    {
        public int DataSourceId { get; set; }

        public DataPaginationModel<Product> Pagination { get; set; }

        public GetAllProductsQuery(int dataSourceId, DataPaginationModel<Product> pagination)
        {
            DataSourceId = dataSourceId;
            Pagination = pagination;
        }
    }
}
