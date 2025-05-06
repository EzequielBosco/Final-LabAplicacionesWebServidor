namespace Order.Application.DTOs.Responses.Order;

public class OrderGetAllResponse
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime DateTime { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public List<OrderProductResponse> Products { get; set; } = new List<OrderProductResponse>();
}

public class OrderProductResponse
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}
