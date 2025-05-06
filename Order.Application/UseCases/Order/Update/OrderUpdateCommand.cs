using MediatR;
using Order.Application.DTOs.Requests.Order;
using Order.Domain.Results.Generic;

namespace Order.Application.UseCases.Order.Update;

public class OrderUpdateCommand(int id, OrderUpdateRequest request) : IRequest<Result<Domain.Results.Unit>>
{
    public int Id { get; } = id;
    public string? Name { get; } = request.Name;
    public string? Code { get; } = request.Code;
    public string? Description { get; } = request.Description;
    public decimal? UnitPrice { get; } = request.UnitPrice;
    public int? Stock { get; } = request.Stock;
    public int? ProductTypeId { get; } = request.ProductTypeId;
}
