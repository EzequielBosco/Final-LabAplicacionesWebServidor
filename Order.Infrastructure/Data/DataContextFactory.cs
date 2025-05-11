using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Order.Infrastructure.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Order.API");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var connectionString = configuration.GetConnectionString("FinalLabAppWebServidorConnectionString");
        optionsBuilder.UseSqlServer(connectionString);

        return new DataContext(optionsBuilder.Options);
    }
}
