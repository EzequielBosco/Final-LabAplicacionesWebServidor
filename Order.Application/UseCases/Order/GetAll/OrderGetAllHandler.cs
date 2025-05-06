using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs.Responses.Order;
using Order.Domain.Repositories;
using Order.Domain.Results;
using Order.Domain.Results.Errors;
using Order.Domain.Results.Generic;

namespace Order.Application.UseCases.Order.GetAll;

public class OrderGetAllHandler(IOrderRepository orderRepository, 
                                ILogger<OrderGetAllHandler> logger) : 
                                IRequestHandler<OrderGetAllQuery, Result<List<OrderGetAllResponse>>>
{
    public async Task<Result<List<OrderGetAllResponse>>> Handle(OrderGetAllQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await orderRepository.GetAll(query.IncludeDeleted);
            if (orders == null || !orders.Any())
            {
                logger.LogWarning("No se encontraron ordenes.");
            }

            var result = orders.Select(order =>
            {
                var orderProducts = order.OrderItems
                .Select(p => new OrderProductResponse
                {
                    ProductId = p.ProductId,
                    Name = p.ProductName,
                    Code = p.ProductCode,
                    UnitPrice = p.ProductPrice,
                    Quantity = p.ProductQuantity,
                    SubTotal = p.ProductPrice * p.ProductQuantity
                }).ToList();

                return new OrderGetAllResponse
                {
                    Id = order.Id,
                    Code = order.Code,
                    DateTime = order.CreatedAt,
                    ClientId = order.ClientId,
                    TotalPrice = order.TotalPrice,
                    ClientName = order.ClientName,
                    Products = orderProducts,
                };
            }).ToList(); 

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener todas las ordenes.";
            logger.LogError(ex, msg);
            return Result.Failure<List<OrderGetAllResponse>>(Error.Unexpected(msg));
        }
    }
}
