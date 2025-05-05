using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Final.Lab.Application.UseCases.Product.Create;

public class ProductCreateHandler(IProductService productService,
                                  IProductTypeService productTypeService,
                                  IProductRepository productRepository, 
                                  IUnitOfWork unitOfWork,
                                  ProductCreateValidation validations,
                                  ILogger<ProductCreateHandler> logger) : 
                                  IRequestHandler<ProductCreateCommand, Result<ProductCreateResponse>>
{
    public async Task<Result<ProductCreateResponse>> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<ProductCreateResponse>(Error.Validation(errors));
            }

            var existsProduct = await productService.ExistsByCode(command.Code);
            if (!existsProduct.IsSuccess)
            {
                logger.LogError("Error en Product ExistsByCode: {Errors}", existsProduct.Errors.JoinMessages());
                return Result.Failure<ProductCreateResponse>(existsProduct.Errors);
            }
            
            if (existsProduct.Value)
            {
                var msg = $"El producto con código {command.Code} ya existe.";
                logger.LogError(msg);
                return Result.Failure<ProductCreateResponse>(Error.Exists(msg));
            }

            var existsProductType = await productTypeService.GetById(command.ProductTypeId);
            if (!existsProductType.IsSuccess)
            {
                logger.LogError("Error en ProductType GetById: {Errors}", existsProductType.Errors.JoinMessages());
                return Result.Failure<ProductCreateResponse>(existsProductType.Errors);
            }
            
            if (existsProductType.Value == null)
            {
                var msg = $"El tipo de producto con id {command.ProductTypeId} no existe.";
                logger.LogError(msg);
                return Result.Failure<ProductCreateResponse>(Error.NotFound(msg));
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

            var create = await productRepository.Create(product);
            if (!create)
            {
                var msg = $"Error al crear el producto en la DB.";
                logger.LogError(msg);
                return Result.Failure<ProductCreateResponse>(Error.Unexpected(msg));
            }

            var save = await unitOfWork.Save();
            if (save == 0)
            {
                var msg = $"Error al guardar el producto en la DB.";
                logger.LogError(msg);
                return Result.Failure<ProductCreateResponse>(Error.Unexpected(msg));
            }

            var result = new ProductCreateResponse
            {
                Id = product.Id,
            };

            return Result.Success(result, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            var msg = "Error al crear el producto.";
            logger.LogError(ex, msg);
            return Result.Failure<ProductCreateResponse>(Error.Unexpected(msg));
        }
    }
}
