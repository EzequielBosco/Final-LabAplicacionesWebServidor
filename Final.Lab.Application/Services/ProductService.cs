using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.Product.ExistsByCode;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
namespace Final.Lab.Application.Services;

public class ProductService(IProductRepository productRepository,
                            ProductGetByIdValidation getByIdValidations,
                            ProductExistsByCodeValidation existsByCodeValidations,
                            ILogger<ProductService> logger) : IProductService
{
    public async Task<bool> ExistsByCode(string code)
    {
        try
        {
            var query = new ProductExistsByCodeQuery(code);
            var validationResult = await existsByCodeValidations.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
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
            throw new Exception(msg);
        }
    }

    public async Task<ProductGetByIdResponse> GetById(int productId)
    {
        try
        {
            var query = new ProductGetByIdQuery(productId);
            var validationResult = await getByIdValidations.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            var product = await productRepository.GetById(query.Id, query.IncludeDeleted);
            if (product == null)
            {
                var msg = $"No se encontró el producto con Id: {query.Id}";
                logger.LogError(msg);
                throw new Exception(msg);
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
            logger.LogError(ex, "Error al obtener el producto por Id.");
            throw;
        }
    }
}
