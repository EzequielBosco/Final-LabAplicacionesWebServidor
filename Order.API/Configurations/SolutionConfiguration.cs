using Order.API.Controllers.Examples.Order;
using Order.Application;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace Order.API.Configurations;

public static class SolutionConfiguration
{
    public static void ConfigureSolution(this WebApplicationBuilder builder)
    {
        builder.ConfigureLogger()
               .ConfigureApplication();

        builder.Services.ConfigureSwagger()
                        .AddApplicationServices(builder.Configuration);
    }

    private static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder builder)
    {
        // Serilog
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();

        return builder;
    }

    private static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        return builder;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        // Swagger y Examples
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.ExampleFilters();
        });
        services.AddSwaggerExamplesFromAssemblyOf<OrderCreateExample>();
        return services;
    }
}
