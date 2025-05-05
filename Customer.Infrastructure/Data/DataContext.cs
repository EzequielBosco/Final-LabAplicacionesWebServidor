using Customer.Domain.Models;
using Customer.Infrastructure.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientSeed());
    }

    public DbSet<Client> Clients { get; set; }
}
