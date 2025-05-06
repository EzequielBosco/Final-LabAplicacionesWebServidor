using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Order.Infrastructure.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContextFactory() { }

    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer("data source=DESKTOP-K143SM2;Database=OrderDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new DataContext(optionsBuilder.Options);
    }
}
