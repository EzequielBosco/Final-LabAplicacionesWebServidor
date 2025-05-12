using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetByIds;

public class ProductGetByIdsHandler(IProductRepository productRepository, 
                                    ILogger<ProductGetByIdsHandler> logger,
                                    ProductGetByIdsValidation validations) : 
                                    IRequestHandler<ProductGetByIdsQuery, Result<List<ProductGetByIdsResponse>>>
{
    public async Task<Result<List<ProductGetByIdsResponse>>> Handle(ProductGetByIdsQuery command, CancellationToken cancellationToken)
    {
        try
		{
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<List<ProductGetByIdsResponse>>(Error.Validation(errors));
            }

            var productIds = command.Ids.Distinct().ToList();
            var products = await productRepository.GetAllBy(p => productIds.Contains(p.Id), includeDeleted: false);
            if (products is null || !products.Any())
            {
                return new List<ProductGetByIdsResponse>();
            }

            var result = products.Select(p => new ProductGetByIdsResponse
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Stock = p.Stock,
                UnitPrice = p.UnitPrice
            }).ToList();

            return result;
        }
		catch (Exception ex)
		{
            var msg = "Error al obtener los productos.";
            logger.LogError(ex, msg);
            return Result.Failure<List<ProductGetByIdsResponse>>(Error.Unexpected(msg));
        }
    }
}
