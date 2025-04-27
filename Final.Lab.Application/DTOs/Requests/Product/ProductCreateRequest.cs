namespace Final.Lab.Application.DTOs.Requests.Product;

public class ProductCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public int ProductTypeId { get; set; }
}
