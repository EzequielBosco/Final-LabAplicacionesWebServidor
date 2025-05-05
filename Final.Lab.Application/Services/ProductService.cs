using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.Product.ExistsByCode;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.Services;

public class ProductService(IProductRepository productRepository,
                            ProductGetByIdValidation getByIdValidations,
                            ProductExistsByCodeValidation existsByCodeValidations,
                            ILogger<ProductService> logger) : IProductService
{
    public async Task<Result<bool>> ExistsByCode(string code)
    {
        try
        {
            var query = new ProductExistsByCodeQuery(code);
            var validationResult = await existsByCodeValidations.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => Error.Validation(e.ErrorMessage));
                return Result.Failure<bool>(errors);
            }

            var existsProduct = await productRepository.ExistsBy(x => x.Code == query.Code);
            if (existsProduct)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener existencia del producto por código.";
            logger.LogError(ex, msg);
            return Result.Failure<bool>(Error.Unexpected(msg));
        }
    }

    public async Task<Result<ProductGetByIdResponse>> GetById(int productId)
    {
        try
        {
            var query = new ProductGetByIdQuery(productId);
            var validationResult = await getByIdValidations.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<ProductGetByIdResponse>(Error.Validation(errors));
            }

            var product = await productRepository.GetById(query.Id, query.IncludeDeleted);
            if (product == null)
            {
                var msg = $"El producto con id {query.Id} no existe.";
                logger.LogError(msg);
                return Result.Failure<ProductGetByIdResponse>(Error.NotFound(msg));
            }

            var result = new ProductGetByIdResponse
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                IsDeleted = product.IsDeleted,
                DeletedAt = product.DeletedAt,
                ProductType = new ProductTypeGetByIdResponse
                {
                    Id = product.ProductTypeId,
                    Name = product.ProductType.Name,
                    Code = product.ProductType.Code,
                    Description = product.ProductType.Description,
                    CreatedAt = product.ProductType.CreatedAt,
                    UpdatedAt = product.ProductType.UpdatedAt,
                    IsDeleted = product.ProductType.IsDeleted,
                    DeletedAt = product.ProductType.DeletedAt
                }
            };

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener el producto por Id.";
            logger.LogError(ex, msg);
            return Result.Failure<ProductGetByIdResponse>(Error.Unexpected(msg));
        }
    }
}
