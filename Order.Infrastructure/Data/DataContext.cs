using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Data.DataSeed;

namespace Order.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderSeed());
        modelBuilder.ApplyConfiguration(new OrderItemSeed());
    }

    public DbSet<Domain.Models.Order> Orders { get; set; }
    public DbSet<Domain.Models.OrderItem> OrderItems { get; set; }
}
