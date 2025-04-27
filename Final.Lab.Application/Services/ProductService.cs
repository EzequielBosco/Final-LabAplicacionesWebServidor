using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
namespace Final.Lab.Application.Services;

public class ProductService(IProductRepository productRepository,
                            ProductGetByIdValidation validations,
                            ILogger<ProductGetByIdHandler> logger) : IProductService
{
    public async Task<ProductGetByIdResponse> GetById(ProductGetByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(query, cancellationToken);

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
            var msg = "Error al obtener el producto por Id.";
            logger.LogError(ex, msg);
            throw new Exception(msg);
        }
    }
}
