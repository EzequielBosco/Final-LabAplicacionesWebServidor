using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.Data.DataSeed;

public class OrderSeed : IEntityTypeConfiguration<Domain.Models.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Order> builder)
    {

        builder.HasData(new Domain.Models.Order
        {
            Id = 1,
            Code = "OR001",
            ClientId = 1,
            ClientName = "Client 1",
            ClientCode = "CL-82A127A2",
            TotalPrice = 100,
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        },
        new Domain.Models.Order
        {
            Id = 2,
            Code = "OR002",
            ClientId = 2,
            ClientName = "Client 2",
            ClientCode = "CL-86A517A9",
            TotalPrice = 40,
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        });
    }
}
