using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Data.DataSeed;

public class OrderItemSeed : IEntityTypeConfiguration<Domain.Models.OrderItem>
{
    public void Configure(EntityTypeBuilder<Domain.Models.OrderItem> builder)
    {

        builder.HasData(new Domain.Models.OrderItem
        {
            Id = 1,
            ProductQuantity = 2,
            ProductId = 1,
            ProductPrice = 10,
            ProductName = "Product 1",
            ProductCode = "P001",
            OrderId = 1
        },
        new Domain.Models.OrderItem
        {
            Id = 2,
            ProductQuantity = 4,
            ProductId = 2,
            ProductPrice = 20,
            ProductName = "Product 2",
            ProductCode = "P002",
            OrderId = 1
        },
        new Domain.Models.OrderItem
        {
            Id = 3,
            ProductQuantity = 2,
            ProductId = 2,
            ProductPrice = 20,
            ProductName = "Product 2",
            ProductCode = "P002",
            OrderId = 2
        });
    }
}
