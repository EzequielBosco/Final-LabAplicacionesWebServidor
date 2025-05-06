using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs.Requests.Product;
using Order.Application.DTOs.Responses.Order;
using Order.Application.Services.Contracts;
using Order.Application.UseCases.Order.Create.Commands;
using Order.Application.UseCases.Order.Create.Validations;
using Order.Domain.Extensions;
using Order.Domain.Repositories;
using Order.Domain.Results;
using Order.Domain.Results.Errors;
using Order.Domain.Results.Generic;
using System.Net;

namespace Order.Application.UseCases.Order.Create;

public class OrderCreateHandler(IOrderRepository orderRepository, 
                                IUnitOfWork unitOfWork,
                                IClientApiService clientApiService,
                                IProductApiService productApiService,
                                OrderCreateValidation validations,
                                ILogger<OrderCreateHandler> logger) : 
                                IRequestHandler<OrderCreateCommand, Result<OrderCreateResponse>>
{
    public async Task<Result<OrderCreateResponse>> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<OrderCreateResponse>(Error.Validation(errors));
            }

            var productsResponse = await productApiService.GetByIds(new ProductGetByIdsRequest
            {
                Ids = command.Products.Select(p => p.ProductId).ToList()
            });
            if (!productsResponse.IsSuccess)
            {
                logger.LogError("Error en ProductApi GetByIds: {Errors}", productsResponse.Errors.JoinMessages());
                return Result.Failure<OrderCreateResponse>(productsResponse.Errors);
            }

            if (productsResponse.Value is null || !productsResponse.Value.Any())
            {
                var msg = $"Error al obtener los productos de ProductApi.";
                logger.LogError(msg);
                return Result.Failure<OrderCreateResponse>(Error.Unexpected(msg));
            }
            var products = productsResponse.Value;

            var clientResponse = await clientApiService.GetById(command.ClientId);
            if (!clientResponse.IsSuccess)
            {
                logger.LogError("Error en ClientApi GetById: {Errors}", clientResponse.Errors.JoinMessages());
                return Result.Failure<OrderCreateResponse>(clientResponse.Errors);
            }

            if (clientResponse.Value is null)
            {
                var msg = $"Error al obtener el cliente con id {command.ClientId} de CustomerApi.";
                logger.LogError(msg);
                return Result.Failure<OrderCreateResponse>(Error.Unexpected(msg));
            }
            var client = clientResponse.Value;

            var orderItems = from p in command.Products
                             join product in products on p.ProductId equals product.Id
                             select new Domain.Models.OrderItem
                             {
                                 ProductId = product.Id,
                                 ProductCode = product.Code,
                                 ProductName = product.Name,
                                 ProductPrice = product.UnitPrice,
                                 ProductQuantity = p.Quantity
                             };

            var order = new Domain.Models.Order
            {
                ClientId = client.Id,
                ClientName = client.FirstName,
                ClientCode = client.Code,
                OrderItems = orderItems.ToList(),
                TotalPrice = orderItems.Sum(i => i.ProductPrice * i.ProductQuantity),
                Code = OrderExtensions.GenerateCode(),
                CreatedAt = DateTime.UtcNow
            };

            var create = await orderRepository.Create(order);
            if (!create)
            {
                var msg = $"Error al crear la orden en la DB.";
                logger.LogError(msg);
                return Result.Failure<OrderCreateResponse>(Error.Unexpected(msg));
            }

            var save = await unitOfWork.Save();
            if (save == 0)
            {
                var msg = $"Error al guardar la orden en la DB.";
                logger.LogError(msg);
                return Result.Failure<OrderCreateResponse>(Error.Unexpected(msg));
            }

            var result = new OrderCreateResponse
            {
                Id = order.Id,
            };

            return Result.Success(result, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            var msg = "Error al crear la orden.";
            logger.LogError(ex, msg);
            return Result.Failure<OrderCreateResponse>(Error.Unexpected(msg));
        }
    }
}
