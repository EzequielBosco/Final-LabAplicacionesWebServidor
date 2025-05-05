using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.ProductType.GetById;

public class ProductTypeGetByIdQuery(int id, bool? includeDeleted = false) : IRequest<Result<ProductTypeGetByIdResponse>>
{
    public int Id { get; } = id;
    public bool? IncludeDeleted { get; } = includeDeleted;
}
