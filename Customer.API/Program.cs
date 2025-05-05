using Customer.API.Controllers.Examples.Client;
using Customer.Application;
using Customer.Application.UseCases.Client.Create;
using Customer.Application.UseCases.Client.ExistsByCode;
using Customer.Application.UseCases.Client.GetById;
using Customer.Application.UseCases.Client.Update;
using Customer.Infrastructure.Data;
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
builder.Services.AddSwaggerExamplesFromAssemblyOf<ClientCreateExample>()
                .AddSwaggerExamplesFromAssemblyOf<ClientUpdateExample>();

//-- DbContext ---------------------------
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinalLabAppWebServidorConnectionString")));

//-- FluentValidation --
builder.Services.AddValidatorsFromAssemblyContaining<ClientGetByIdValidation>()
                .AddValidatorsFromAssemblyContaining<ClientExistsByCodeValidation>()
                .AddValidatorsFromAssemblyContaining<ClientCreateValidation>()
                .AddValidatorsFromAssemblyContaining<ClientUpdateValidation>();
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

builder.Services
//-- Repositories ------------------------
    .AddScoped<Customer.Domain.Repositories.IClientRepository, Customer.Infrastructure.Repositories.ClientRepository>()
//-- Services ----------------------------
    .AddScoped<Customer.Application.Services.Contracts.IClientService, Customer.Application.Services.ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//-- Custom Middleware --------------------
app.UseMiddleware<Customer.API.Middlewares.ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
