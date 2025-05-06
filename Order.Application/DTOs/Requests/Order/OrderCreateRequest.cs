namespace Order.Application.DTOs.Requests.Order;

public class OrderCreateRequest
{
    public int ClientId { get; set; }
    public List<OrderCreateProductItem> Products { get; set; } = new();
}

public class OrderCreateProductItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}