using Productos.BackEnd.Application.Features.Products.Validators;
using Productos.BackEnd.Application.Registration;
using Productos.BackEnd.Business.Registration;
using Productos.BackEnd.Infrastructure.Registration;
using Productos.BackEnd.WebAPI.Builders;
using Productos.BackEnd.WebAPI.Registration;
using WatchDog;
using FluentValidation.AspNetCore;
using FluentValidation;
using Productos.BackEnd.Application.Features.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddValidatorsFromAssemblyContaining<ProductModelValidator>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddWatchDogServices();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ProductHandler>());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddSwaggerApp();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
