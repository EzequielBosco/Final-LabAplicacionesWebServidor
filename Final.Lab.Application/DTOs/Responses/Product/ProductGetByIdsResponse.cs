namespace Final.Lab.Application.DTOs.Responses.Product;

public class ProductGetByIdsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
}
