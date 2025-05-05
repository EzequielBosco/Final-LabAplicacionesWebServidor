namespace Final.Lab.Application.DTOs.Responses.Product;

public class ProductGetByTypeIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public bool IsDeleted { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; } = string.Empty;
}
