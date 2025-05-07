using Final.Lab.Application.DTOs.Requests.Product;
using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetByIds;

public class ProductGetByIdsQuery(ProductGetByIdsRequest request) : IRequest<Result<List<ProductGetByIdsResponse>>>
{
    public List<int> Ids { get; } = request.Ids;
}
