using Final.Lab.Application.DTOs.Requests.Product;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.Create;

public class ProductCreateCommand(ProductCreateRequest request) : IRequest<bool>
{
    public string Name { get; } = request.Name;
    public string Code { get; } = request.Code;
    public string? Description { get; } = request.Description;
    public decimal UnitPrice { get; } = request.UnitPrice;
    public int Stock { get; } = request.Stock;
    public int ProductTypeId { get; } = request.ProductTypeId;
}
