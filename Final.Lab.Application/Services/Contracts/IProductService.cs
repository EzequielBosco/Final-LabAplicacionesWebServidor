using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.UseCases.Product.GetById;

namespace Final.Lab.Application.Services.Contracts;

public interface IProductService
{
    Task<ProductGetByIdResponse> GetById(ProductGetByIdQuery query, CancellationToken cancellationToken);
}
