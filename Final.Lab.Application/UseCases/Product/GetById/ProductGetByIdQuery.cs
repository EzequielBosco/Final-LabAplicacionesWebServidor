using Final.Lab.Application.DTOs.Responses;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdQuery(int id) : IRequest<ProductGetByIdResponse>
{
    public int Id { get; } = id;
}
