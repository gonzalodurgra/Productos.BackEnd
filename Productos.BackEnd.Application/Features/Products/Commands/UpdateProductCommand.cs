using MediatR;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Features.Products.Commands
{
    /// <summary>
    /// Instrucción para actualizar un producto por el método PUT
    /// </summary>
    public class UpdateProductCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock {  get; set; }

        public AuditModel Audit { get; set; } = default!;

        public UpdateProductCommand(int id, string name, decimal price, int stock) 
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
