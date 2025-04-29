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
    /// Instrucción para borrar producto por el método DELETE
    /// </summary>
    public class DeleteProductCommand : IRequest<OkResponseModel>
    {
        public int Id { get; set; }

        public AuditModel Audit { get; set; } = default!;

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
