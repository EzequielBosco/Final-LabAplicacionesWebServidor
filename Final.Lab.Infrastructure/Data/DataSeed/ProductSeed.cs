using Final.Lab.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Final.Lab.Infrastructure.Data.DataSeed;

public class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.HasData(new Product
        {
            Id = 1,
            Code = "P001",
            Name = "Product 1",
            Description = "Description for Product 1",
            UnitPrice = 10.00m,
            Stock = 100,
            ProductTypeId = 1,
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        },
        new Product
        {
            Id = 2,
            Code = "P002",
            Name = "Product 2",
            Description = "Description for Product 2",
            UnitPrice = 20.00m,
            Stock = 150,
            ProductTypeId = 2,
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        });
    }
}
