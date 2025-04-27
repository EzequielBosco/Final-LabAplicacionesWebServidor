using Final.Lab.Application.DTOs.Responses.Product;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetAll;

public class ProductGetAllQuery(bool? includeDeleted) : IRequest<List<ProductGetAllResponse>>
{
    public bool? IncludeDeleted { get; } = includeDeleted;
}
