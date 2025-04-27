using Final.Lab.Application.DTOs.Responses.ProductType;
using MediatR;

namespace Final.Lab.Application.UseCases.ProductType.GetById;

public class ProductTypeGetByIdQuery(int id, bool? includeDeleted = false) : IRequest<ProductTypeGetByIdResponse>
{
    public int Id { get; } = id;
    public bool? IncludeDeleted { get; } = includeDeleted;
}
