using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        // DataContext
        var connectionString = configuration.GetConnectionString(connectionStringName);
        services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
