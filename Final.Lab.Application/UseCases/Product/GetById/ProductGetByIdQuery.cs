using Final.Lab.Application.DTOs.Responses.Product;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdQuery(int id, bool? includeDeleted) : IRequest<ProductGetByIdResponse>
{
    public int Id { get; } = id;
    public bool? IncludeDeleted { get; } = includeDeleted;
}
