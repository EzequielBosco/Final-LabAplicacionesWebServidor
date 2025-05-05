using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdQuery(int id, bool? includeDeleted = false) : IRequest<Result<ProductGetByIdResponse>>
{
    public int Id { get; } = id;
    public bool? IncludeDeleted { get; } = includeDeleted;
}
