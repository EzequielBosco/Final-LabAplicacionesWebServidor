using Final.Lab.Application.DTOs.Responses.ProductType;

namespace Final.Lab.Application.DTOs.Responses.Product;

public class ProductGetAllResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public ProductTypeGetAllResponse ProductType { get; set; } = new ProductTypeGetAllResponse();
}
