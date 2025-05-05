using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Final.Lab.Application.UseCases.Product.SoftDelete;

public class ProductSoftDeleteHandler(IProductRepository productRepository,
                                      ProductSoftDeleteValidation validations,
                                      IUnitOfWork unitOfWork,
                                      ILogger<ProductSoftDeleteHandler> logger) : 
                                      IRequestHandler<ProductSoftDeleteCommand, Result<Domain.Results.Unit>>
{
    public async Task<Result<Domain.Results.Unit>> Handle(ProductSoftDeleteCommand command, CancellationToken cancellationToken)
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
                var msg = $"Error al obtener el producto con Id: {command.Id}.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.NotFound(msg));
            }

            var result = await productRepository.SoftDelete(product);
            if (!result)
            {
                var msg = $"Error al eliminar logicamente el producto en la DB.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
            }

            var save = await unitOfWork.Save();
            if (save == 0)
            {
                var msg = $"Error al guardar el producto en la DB.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
            }

            return Result.Success(HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            var msg = "Error al eliminar logicamente el producto.";
            logger.LogError(ex, msg);
            return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
        }
    }
}
