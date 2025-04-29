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
    /// Instrucción para recibir un solo producto por GET
    /// </summary>
    public class GetProductsByIdQuery : IRequest<ProductResponseModel>
    {
        public int Id { get; set; }

        public GetProductsByIdQuery(int id) 
        {
            Id = id;
        }
    }
}
