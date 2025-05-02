using Final.Lab.Application;
using Final.Lab.Application.UseCases.Product.Create;
using Final.Lab.Application.UseCases.Product.ExistsByCode;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Application.UseCases.Product.Update;
using Final.Lab.Application.UseCases.ProductType.GetById;
using Final.Lab.Infrastructure.Data;
using Final_LabAplicacionesWebServidor.Controllers.Examples.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//-- Examples ----------------------------
builder.Services.AddSwaggerGen(x =>
{
    x.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProductCreateExample>()
                .AddSwaggerExamplesFromAssemblyOf<ProductUpdateExample>();

//-- DbContext ---------------------------
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinalLabAppWebServidorConnectionString")));

//-- FluentValidation --
builder.Services.AddValidatorsFromAssemblyContaining<Final.Lab.Domain.Validations.ProductValidator>()
                .AddValidatorsFromAssemblyContaining<ProductGetByIdValidation>()
                .AddValidatorsFromAssemblyContaining<ProductTypeGetByIdValidation>()
                .AddValidatorsFromAssemblyContaining<ProductExistsByCodeValidation>()
                .AddValidatorsFromAssemblyContaining<ProductCreateValidation>()
                .AddValidatorsFromAssemblyContaining<ProductUpdateValidation>();
builder.Services.AddFluentValidationAutoValidation();

//-- MediatR -----------------------------
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.RegisterServicesFromAssembly(typeof(Register).Assembly);
});

//-- Serilog -----------------------------
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
builder.Logging.ClearProviders();
builder.Host.UseSerilog();

//-- Unit of Work ------------------------
builder.Services.AddScoped<Final.Lab.Domain.Repositories.IUnitOfWork, Final.Lab.Infrastructure.Repositories.UnitOfWork>();

builder.Services
//-- Repositories ------------------------
    .AddScoped<Final.Lab.Domain.Repositories.IProductRepository, Final.Lab.Infrastructure.Repositories.ProductRepository>()
    .AddScoped<Final.Lab.Domain.Repositories.IProductTypeRepository, Final.Lab.Infrastructure.Repositories.ProductTypeRepository>()
//-- Services ----------------------------
    .AddScoped<Final.Lab.Application.Services.Contracts.IProductService, Final.Lab.Application.Services.ProductService>()
    .AddScoped<Final.Lab.Application.Services.Contracts.IProductTypeService, Final.Lab.Application.Services.ProductTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
