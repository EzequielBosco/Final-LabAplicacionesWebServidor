using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.Product.Update;
using Final.Lab.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.Create;

public class ProductCreateHandler(IProductService productService,
                                  IProductRepository productRepository, 
                                  IUnitOfWork unitOfWork,
                                  ProductCreateValidation validations,
                                  ILogger<ProductUpdateHandler> logger) : 
                                  IRequestHandler<ProductCreateCommand, bool>
{
    public async Task<bool> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            var existsProduct = productService.ExistsByCode(command.Code).Result;
            if (existsProduct)
            {
                var msg = $"El producto con código {command.Code} ya existe.";
                logger.LogError(msg);
                throw new Exception(msg);
            }

            var existsProductType = productService.GetById(command.ProductTypeId).Result;
            if (existsProductType == null)
            {
                var msg = $"El tipo de producto con id {command.ProductTypeId} no existe.";
                logger.LogError(msg);
                throw new Exception(msg);
            }

            var product = new Domain.Models.Product
            {
                Name = command.Name,
                Code = command.Code,
                Description = command.Description,
                UnitPrice = command.UnitPrice,
                Stock = command.Stock,
                ProductTypeId = command.ProductTypeId,
                CreatedAt = DateTime.UtcNow
            };

            var result = await productRepository.Create(product);
            await unitOfWork.Save();
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al crear el producto.");
            throw;
        }    
    }
}
