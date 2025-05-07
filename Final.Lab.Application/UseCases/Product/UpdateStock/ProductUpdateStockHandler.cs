using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.UseCases.Product.UpdateStock.Commands;
using Final.Lab.Application.UseCases.Product.UpdateStock.Validations;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.UpdateStock;

public class ProductUpdateStockHandler(IProductRepository productRepository,
                                       ProductUpdateStockValidation validations,
                                       IUnitOfWork unitOfWork,
                                       ILogger<ProductUpdateStockHandler> logger) : 
                                       IRequestHandler<ProductUpdateStockCommand, Result<List<ProductUpdateStockResponse>>>
{
    public async Task<Result<List<ProductUpdateStockResponse>>> Handle(ProductUpdateStockCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validations.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.JoinMessages();
            logger.LogError("Errores de validación: {Errors}", errors);
            return Result.Failure<List<ProductUpdateStockResponse>>(Error.Validation(errors));
        }

        var result = new List<ProductUpdateStockResponse>();

        foreach (var item in command.Products)
        {
            var product = await productRepository.GetById(item.ProductId);
            if (product == null)
            {
                var msg = $"Error al obtener el producto con Id: {item.ProductId}.";
                logger.LogError(msg);
                return Result.Failure<List<ProductUpdateStockResponse>>(Error.NotFound(msg));
            }

            var quantityToSubtract = Math.Min(item.QuantityToSubtract, product.Stock);

            if (quantityToSubtract > 0)
            {
                product.Stock -= quantityToSubtract;

                var patch = await productRepository.Patch(product, prod =>
                {
                    prod.Stock = product.Stock;
                    prod.UpdatedAt = DateTime.UtcNow;
                });
                if (!patch)
                {
                    var msg = $"Error al actualizar el stock del producto con Id: {item.ProductId}.";
                    logger.LogError(msg);
                    return Result.Failure<List<ProductUpdateStockResponse>>(Error.Unexpected(msg));
                }

                var save = await unitOfWork.Save();
                if (save == 0)
                {
                    var msg = $"Error al guardar los cambios en la DB.";
                    logger.LogError(msg);
                    return Result.Failure<List<ProductUpdateStockResponse>>(Error.Unexpected(msg));
                }
            }

            result.Add(new ProductUpdateStockResponse
            {
                ProductId = product.Id,
                QuantitySubtracted = quantityToSubtract,
                RemainingStock = product.Stock
            });
        }

        return result;
    }
}
