using MediatR;
using Order.Application.DTOs.Requests.Order;
using Order.Application.DTOs.Responses.Order;
using Order.Domain.Results.Generic;

namespace Order.Application.UseCases.Order.Create.Commands;

public class OrderCreateCommand(OrderCreateRequest request) : IRequest<Result<OrderCreateResponse>>
{
    public int ClientId { get; } = request.ClientId;
    public List<OrderCreateProductCommand> Products { get; } = request.Products.Select(p => new OrderCreateProductCommand(p)).ToList();
}
