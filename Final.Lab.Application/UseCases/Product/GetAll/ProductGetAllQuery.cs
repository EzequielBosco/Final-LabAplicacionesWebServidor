using Final.Lab.Application.DTOs.Responses;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetAll;

public class ProductGetAllQuery : IRequest<List<ProductResponse>>
{
}
