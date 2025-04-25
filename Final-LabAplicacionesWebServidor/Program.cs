using Final.Lab.Application;
using Final.Lab.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-- DbContext ---------------------------
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinalLabAppWebServidorConnectionString")));

//-- FluentValidation --
builder.Services.AddValidatorsFromAssemblyContaining<Final.Lab.Domain.Validations.ProductValidator>();

//-- MediatR -----------------------------
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.RegisterServicesFromAssembly(typeof(Register).Assembly);
});

//-- Serilog -----------------------------
builder.Logging.ClearProviders();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
builder.Host.UseSerilog();

builder.Services
//-- Repositories ------------------------
    .AddScoped<Final.Lab.Domain.Repositories.IProductRepository, Final.Lab.Infrastructure.Repositories.ProductRepository>()
//-- Services ----------------------------
    .AddScoped<Final.Lab.Application.Services.Contracts.IProductService, Final.Lab.Application.Services.ProductService>();

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
