using FluentValidation;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Features.Products.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator() 
        { 
            RuleFor(p => p.Name).NotEmpty().WithMessage("El nombre del producto es obligatorio").MaximumLength(30);
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("El precio debe ser mayor que 0").PrecisionScale(5, 2, false).WithMessage("Precio menor a 999.99 con 2 decimales");
            RuleFor(p => p.Stock).GreaterThan(0).WithMessage("Debe haber un stock superior a 0");
        }
    }
}
