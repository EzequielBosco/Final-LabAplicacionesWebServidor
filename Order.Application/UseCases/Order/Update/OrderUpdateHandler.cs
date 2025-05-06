using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain.Extensions;
using Order.Domain.Repositories;
using Order.Domain.Results;
using Order.Domain.Results.Errors;
using Order.Domain.Results.Generic;

namespace Order.Application.UseCases.Order.Update;

public class OrderUpdateHandler(IOrderRepository productRepository,
                                IUnitOfWork unitOfWork,
                                OrderUpdateValidation validations,
                                ILogger<OrderUpdateHandler> logger) : 
                                IRequestHandler<OrderUpdateCommand, Result<Domain.Results.Unit>>
{
    public async Task<Result<Domain.Results.Unit>> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<Domain.Results.Unit>(Error.Validation(errors));
            }

            var product = await productRepository.GetById(command.Id);
            if (product == null)
            {
                var msg = $"Error al obtener la orden con Id: {command.Id}.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.NotFound(msg));
            }

            product.Code = command.Code ?? product.Code;
            product.UpdatedAt = DateTime.UtcNow;

            var save = await unitOfWork.Save();
            if (save == 0)
            {
                var msg = "Error al guardar los cambios en la DB.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            var msg = "Error al actualizar la orden.";
            logger.LogError(ex, msg);
            return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
        }
    }
}
