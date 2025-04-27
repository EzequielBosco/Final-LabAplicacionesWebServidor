namespace Final.Lab.Application.DTOs.Requests.Product;

public class ProductUpdateRequest
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? Stock { get; set; }
    public int? ProductTypeId { get; set; }
}