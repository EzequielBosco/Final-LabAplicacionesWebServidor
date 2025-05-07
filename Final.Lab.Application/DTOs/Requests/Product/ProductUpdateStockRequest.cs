namespace Final.Lab.Application.DTOs.Requests.Product;

public class ProductUpdateStockRequest
{
    public List<ProductUpdateStockItem> Products { get; set; } = new();
}

public class ProductUpdateStockItem
{
    public int ProductId { get; set; }
    public int QuantityToSubtract { get; set; }
}
