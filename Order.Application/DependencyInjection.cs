using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Services;
using Order.Application.Services.Contracts;
using Order.Application.UseCases.Order.Create.Validations;
using Order.Infrastructure;
using System.Reflection;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, string connectionString = "FinalLabAppWebServidorConnectionString")
    {
        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<OrderCreateValidation>();
        services.AddFluentValidationAutoValidation();

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssembly(typeof(Register).Assembly);
        });

        // Services 
        services.AddScoped<IClientApiService, ClientApiService>()
                .AddScoped<IProductApiService, ProductApiService>();
        services.AddHttpClient<IProductApiService, ProductApiService>();
        services.AddHttpClient<IClientApiService, ClientApiService>();

        // Infrastructure
        services.AddInfrastructureData(configuration, connectionString);

        return services;
    }
}
