namespace Order.Application.DTOs.Responses.Product;

public class ProductUpdateStockResponse
{
    public int ProductId { get; set; }
    public int QuantitySubtracted { get; set; }
    public int RemainingStock { get; set; }
}

