using Final.Lab.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.Update;

public class ProductUpdateHandler(IProductRepository productRepository,
                                  IUnitOfWork unitOfWork,
                                  ProductUpdateValidation validations,
                                  ILogger<ProductUpdateHandler> logger) : 
                                  IRequestHandler<ProductUpdateCommand, bool>
{
    public async Task<bool> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            var product = productRepository.GetById(command.Id).Result;
            if (product == null)
            {
                var msg = "Error al obtener el producto.";
                logger.LogError(msg);
                throw new Exception(msg);
            }

            product.Name = command.Name ?? product.Name;
            product.Code = command.Code ?? product.Code;
            product.Description = command.Description ?? product.Description;
            product.UnitPrice = command.UnitPrice ?? product.UnitPrice;
            product.Stock = command.Stock ?? product.Stock;
            product.ProductTypeId = command.ProductTypeId ?? product.ProductTypeId;
            product.UpdatedAt = DateTime.UtcNow;

            await unitOfWork.Save();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al actualizar el producto.");
            throw;
        }
    }
}
