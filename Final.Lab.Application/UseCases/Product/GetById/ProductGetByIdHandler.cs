using Final.Lab.Application.DTOs.Responses;
using Final.Lab.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdHandler(IProductRepository productRepository, 
                                   ProductGetByIdValidation validations,
                                   ILogger<ProductGetByIdHandler> logger) : 
                                   IRequestHandler<ProductGetByIdQuery, ProductGetByIdResponse>
{
    public async Task<ProductGetByIdResponse> Handle(ProductGetByIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await validations.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Errores de validación: {errors}");
        }

        var product = await productRepository.GetById(query.Id);
        if (product == null)
        {
            var msg = $"No se encontró el producto con Id: {query.Id}";
            logger.LogError(msg);
            throw new Exception(msg);
        }

        var result = new ProductGetByIdResponse
        {
            Name = product.Name,
            Code = product.Code,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            Stock = product.Stock,
            ProductTypeId = product.ProductTypeId
        };

        return result;
    }
}
