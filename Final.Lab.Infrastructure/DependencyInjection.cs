using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Final.Lab.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Final.Lab.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        // DataContext
        var connectionString = configuration.GetConnectionString(connectionStringName);
        services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IProductTypeRepository, ProductTypeRepository>();

        return services;
    }
}
