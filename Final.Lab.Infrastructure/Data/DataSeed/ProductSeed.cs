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
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        });
    }
}
