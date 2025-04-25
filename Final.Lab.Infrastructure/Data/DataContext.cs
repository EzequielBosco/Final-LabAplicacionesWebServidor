using Final.Lab.Domain.Models;
using Final.Lab.Infrastructure.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace Final.Lab.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductTypeSeed());
        modelBuilder.ApplyConfiguration(new ProductSeed());
    }

    public DbSet<Product> Products { get; set; }
}
