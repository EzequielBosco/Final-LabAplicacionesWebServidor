using Order.Application.DTOs.Requests.Order;

namespace Order.Application.UseCases.Order.Create.Commands;

public class OrderCreateProductCommand(OrderCreateProductItem productItem)
{
    public int ProductId { get; } = productItem.ProductId;
    public int Quantity { get; } = productItem.Quantity;
}
