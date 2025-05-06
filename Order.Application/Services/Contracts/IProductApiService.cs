using Order.Application.DTOs.Requests.Product;
using Order.Application.DTOs.Responses.Product;
using Order.Domain.Results.Generic;

namespace Order.Application.Services.Contracts;

public interface IProductApiService
{
    Task<Result<List<ProductGetByIdsResponse>>> GetByIds(ProductGetByIdsRequest request);
}
