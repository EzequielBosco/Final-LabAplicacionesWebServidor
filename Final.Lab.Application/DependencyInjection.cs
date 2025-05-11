using Final.Lab.Application.Services;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.Product.Create;
using Final.Lab.Application.UseCases.Product.ExistsByCode;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Application.UseCases.Product.Update;
using Final.Lab.Application.UseCases.ProductType.GetById;
using Final.Lab.Domain.Validations;
using Final.Lab.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Final.Lab.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, string connectionString = "FinalLabAppWebServidorConnectionString")
    {
        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<ProductValidator>()
                .AddValidatorsFromAssemblyContaining<ProductGetByIdValidation>()
                .AddValidatorsFromAssemblyContaining<ProductTypeGetByIdValidation>()
                .AddValidatorsFromAssemblyContaining<ProductExistsByCodeValidation>()
                .AddValidatorsFromAssemblyContaining<ProductCreateValidation>()
                .AddValidatorsFromAssemblyContaining<ProductUpdateValidation>();
        services.AddFluentValidationAutoValidation();

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssembly(typeof(Register).Assembly);
        });

        // Services 
        services.AddScoped<IProductService, ProductService>()
                .AddScoped<IProductTypeService, ProductTypeService>();

        // Infrastructure
        services.AddInfrastructureData(configuration, connectionString);

        return services;
    }
}
