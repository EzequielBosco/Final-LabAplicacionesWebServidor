using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Final.Lab.Infrastructure.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer("data source=DESKTOP-K143SM2;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new DataContext(optionsBuilder.Options);
    }
}
