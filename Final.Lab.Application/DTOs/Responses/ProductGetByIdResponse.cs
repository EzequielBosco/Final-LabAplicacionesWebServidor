namespace Final.Lab.Application.DTOs.Responses;

public class ProductGetByIdResponse
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public int ProductTypeId { get; set; }
}
