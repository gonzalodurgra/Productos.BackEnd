using AutoMapper;
using Productos.BackEnd.Application.Features.Products.Commands;
using Productos.BackEnd.Domain.Contracts.Services;
using Productos.BackEnd.Domain.Entities;
using Productos.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BackEnd.Application.Mappings
{
    /// <summary>
    /// Clase que permite el mapeo automático de las diferentes clases de entidad, modelo y órdenes
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductModel>().ReverseMap();

            CreateMap<ProductModel, ProductResponseModel>().ReverseMap();

            CreateMap<ProductModel, CreateProductCommand>().ReverseMap();

            CreateMap<ProductModel,  UpdateProductCommand>().ReverseMap();

            CreateMap<ProductModel, DeleteProductCommand>().ReverseMap();

            CreateMap<Product, ProductResponseModel>().ReverseMap();

            CreateMap<CreateProductCommand, Product>().ReverseMap();

            CreateMap<UpdateProductCommand, Product>().ReverseMap();

            CreateMap<DeleteProductCommand, Product>().ReverseMap();
        }
    }
}
