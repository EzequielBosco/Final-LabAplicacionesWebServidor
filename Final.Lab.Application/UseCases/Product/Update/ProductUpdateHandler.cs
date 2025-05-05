using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.Update;

public class ProductUpdateHandler(IProductRepository productRepository,
                                  IProductTypeService productTypeService,
                                  IUnitOfWork unitOfWork,
                                  ProductUpdateValidation validations,
                                  ILogger<ProductUpdateHandler> logger) : 
                                  IRequestHandler<ProductUpdateCommand, Result<Domain.Results.Unit>>
{
    public async Task<Result<Domain.Results.Unit>> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
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

            if (command.ProductTypeId.HasValue)
            {
                var existsProductType = await productTypeService.GetById(command.ProductTypeId.Value);
                if (!existsProductType.IsSuccess)
                {
                    logger.LogError("Error en ProductType GetById: {Errors}", existsProductType.Errors.JoinMessages());
                    return Result.Failure<Domain.Results.Unit>(existsProductType.Errors);
                }
                else if (existsProductType.Value == null)
                {
                    var msg = $"El tipo de producto con id {command.ProductTypeId} no existe.";
                    logger.LogError(msg);
                    return Result.Failure<Domain.Results.Unit>(Error.NotFound(msg));
                }
            }

            product.Name = command.Name ?? product.Name;
            product.Code = command.Code ?? product.Code;
            product.Description = command.Description ?? product.Description;
            product.UnitPrice = command.UnitPrice ?? product.UnitPrice;
            product.Stock = command.Stock ?? product.Stock;
            product.ProductTypeId = command.ProductTypeId ?? product.ProductTypeId;
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
            var msg = "Error al actualizar el producto.";
            logger.LogError(ex, msg);
            return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
        }
    }
}
