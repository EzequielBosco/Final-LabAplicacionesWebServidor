using Customer.Domain.Repositories;
using Customer.Infrastructure.Data;
using Customer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        // DataContext
        var connectionString = configuration.GetConnectionString(connectionStringName);
        services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

        // Repositories
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}
