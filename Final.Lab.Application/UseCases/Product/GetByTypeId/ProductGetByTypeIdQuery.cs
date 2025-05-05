using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetByProductTypeId;

public class ProductGetByTypeIdQuery(int productTypeId, bool? includeDeleted = false) : IRequest<Result<List<ProductGetByTypeIdResponse>>>
{
    public int ProductTypeId { get; } = productTypeId;
    public bool? IncludeDeleted { get; } = includeDeleted;
}
