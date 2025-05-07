using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Order.API.Controllers.Examples.Order;
using Order.Application;
using Order.Application.UseCases.Order.Create.Validations;
using Order.Infrastructure.Data;
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
builder.Services.AddSwaggerExamplesFromAssemblyOf<OrderCreateExample>();

//-- DbContext ---------------------------
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinalLabAppWebServidorConnectionString")));

//-- FluentValidation --
builder.Services.AddValidatorsFromAssemblyContaining<OrderCreateValidation>();
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
builder.Services.AddScoped<Order.Domain.Repositories.IUnitOfWork,Order.Infrastructure.Repositories.UnitOfWork>();

builder.Services
//-- Repositories ------------------------
    .AddScoped<Order.Domain.Repositories.IOrderRepository, Order.Infrastructure.Repositories.OrderRepository>()
//-- Services ----------------------------
    .AddScoped<Order.Application.Services.Contracts.IClientApiService, Order.Application.Services.ClientApiService>()
    .AddScoped<Order.Application.Services.Contracts.IProductApiService, Order.Application.Services.ProductApiService>();
builder.Services.AddHttpClient<Order.Application.Services.Contracts.IProductApiService, Order.Application.Services.ProductApiService>();
builder.Services.AddHttpClient<Order.Application.Services.Contracts.IClientApiService, Order.Application.Services.ClientApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//-- Custom Middleware --------------------
app.UseMiddleware<Order.API.Middlewares.ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
