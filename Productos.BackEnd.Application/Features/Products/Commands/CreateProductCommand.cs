using MediatR;
using Productos.BackEnd.Domain.Models;

namespace Productos.BackEnd.Application.Features.Products.Commands
{
    /// <summary>
    /// Instrucción para insertar producto por POST
    /// </summary>
    public class CreateProductCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock {  get; set; }

        public AuditModel Audit { get; set; } = default!;

        public CreateProductCommand (int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
