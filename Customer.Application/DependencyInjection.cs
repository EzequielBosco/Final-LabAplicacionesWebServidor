using Customer.Application.Services;
using Customer.Application.Services.Contracts;
using Customer.Application.UseCases.Client.Create;
using Customer.Application.UseCases.Client.ExistsByCode;
using Customer.Application.UseCases.Client.GetById;
using Customer.Application.UseCases.Client.Update;
using Customer.Domain.Repositories;
using Customer.Infrastructure;
using Customer.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Customer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, string connectionString = "FinalLabAppWebServidorConnectionString")
    {
        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<ClientGetByIdValidation>()
                        .AddValidatorsFromAssemblyContaining<ClientExistsByCodeValidation>()
                        .AddValidatorsFromAssemblyContaining<ClientCreateValidation>()
                        .AddValidatorsFromAssemblyContaining<ClientUpdateValidation>();
        services.AddFluentValidationAutoValidation();

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssembly(typeof(Register).Assembly);
        });

        // Services
        services.AddScoped<IClientService, ClientService>();

        // DataContext
        services.AddInfrastructureData(configuration, connectionString);

        return services;
    }
}
