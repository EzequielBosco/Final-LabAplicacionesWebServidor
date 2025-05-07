using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Application.UseCases.ProductType.GetById;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.Services;

public class ProductTypeService(IProductTypeRepository productTypeRepository,
                                ProductTypeGetByIdValidation getByIdValidations,
                                ILogger<ProductTypeService> logger) : IProductTypeService
{
    public async Task<Result<ProductTypeGetByIdResponse>> GetById(int productTypeId, bool? includeDeleted)
    {
        try
        {
            var query = new ProductTypeGetByIdQuery(productTypeId, includeDeleted);
            var validationResult = await getByIdValidations.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<ProductTypeGetByIdResponse>(Error.Validation(errors));
            }

            var product = await productTypeRepository.GetById(query.Id, query.IncludeDeleted);
            if (product == null)
            {
                var msg = $"El tipo de producto con Id: {query.Id} no existe.";
                logger.LogError(msg);
                return Result.Failure<ProductTypeGetByIdResponse>(Error.NotFound(msg));
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
            var msg = "Error al obtener el tipo de producto por Id.";
            logger.LogError(ex, msg);
            return Result.Failure<ProductTypeGetByIdResponse>(Error.Unexpected(msg));
        }
    }
}
