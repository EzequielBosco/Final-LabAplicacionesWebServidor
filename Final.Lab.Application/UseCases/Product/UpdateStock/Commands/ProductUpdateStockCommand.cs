using Final.Lab.Application.DTOs.Requests.Product;
using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.UpdateStock.Commands;

public class ProductUpdateStockCommand(ProductUpdateStockRequest request) : IRequest<Result<List<ProductUpdateStockResponse>>>
{
    public List<ProductUpdateStockItemCommand> Products { get; set; } = request.Products.Select(x => new ProductUpdateStockItemCommand(x)).ToList();
}
