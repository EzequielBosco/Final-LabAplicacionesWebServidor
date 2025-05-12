using Final.Lab.Application.DTOs.Responses.ProductType;

namespace Final.Lab.Application.DTOs.Responses.Product;

public class ProductGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ProductTypeGetByIdResponse ProductType { get; set; } = new();
}
