using Final.Lab.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Final.Lab.Infrastructure.Data.DataSeed;

public class ProductTypeSeed : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {

        builder.HasData(new ProductType
        {
            Id = 1,
            Name = "Type 1",
            Code = "T001",
            Description = "Description for Type 1",
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        },
        new ProductType
        {
            Id = 2,
            Name = "Type 2",
            Code = "T002",
            Description = "Description for Type 2",
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        });
    }
}
