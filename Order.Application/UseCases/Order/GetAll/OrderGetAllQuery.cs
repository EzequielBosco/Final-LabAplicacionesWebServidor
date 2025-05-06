using MediatR;
using Order.Application.DTOs.Responses.Order;
using Order.Domain.Results.Generic;

namespace Order.Application.UseCases.Order.GetAll;

public class OrderGetAllQuery(bool? includeDeleted) : IRequest<Result<List<OrderGetAllResponse>>>
{
    public bool? IncludeDeleted { get; } = includeDeleted;
}
