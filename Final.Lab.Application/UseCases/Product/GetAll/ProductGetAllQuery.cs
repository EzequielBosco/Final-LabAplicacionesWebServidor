using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetAll;

public class ProductGetAllQuery(bool? includeDeleted) : IRequest<Result<List<ProductGetAllResponse>>>
{
    public bool? IncludeDeleted { get; } = includeDeleted;
}
