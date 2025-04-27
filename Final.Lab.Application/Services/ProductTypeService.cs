using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.ProductType.GetById;
using Final.Lab.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.Services;

public class ProductTypeService(IProductTypeRepository productTypeRepository,
                                ProductTypeGetByIdValidation getByIdValidations,
                                ILogger<ProductTypeService> logger) : IProductTypeService
{
    public async Task<ProductTypeGetByIdResponse> GetById(int productTypeId)
    {
        try
        {
            var query = new ProductTypeGetByIdQuery(productTypeId);
            var validationResult = await getByIdValidations.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            var product = await productTypeRepository.GetById(query.Id, query.IncludeDeleted);
            if (product == null)
            {
                var msg = $"No se encontró el tipo de producto con Id: {query.Id}";
                logger.LogError(msg);
                throw new Exception(msg);
            }

            var result = new ProductTypeGetByIdResponse
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                IsDeleted = product.IsDeleted,  
                DeletedAt = product.DeletedAt
            };

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al obtener el tipo de producto por Id.");
            throw;
        }
    }
}
