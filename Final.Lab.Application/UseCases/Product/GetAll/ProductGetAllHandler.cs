using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetAll;

public class ProductGetAllHandler(IProductRepository productRepository, 
                                  ILogger<ProductGetAllHandler> logger) : 
                                  IRequestHandler<ProductGetAllQuery, Result<List<ProductGetAllResponse>>>
{
    public async Task<Result<List<ProductGetAllResponse>>> Handle(ProductGetAllQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var products = await productRepository.GetAll(query.IncludeDeleted);
            if (products == null || !products.Any())
            {
                logger.LogWarning("No se encontraron productos.");
            }

            var result = products.Select(x => new ProductGetAllResponse
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                Stock = x.Stock,
                ProductType = new ProductTypeGetAllResponse
                {
                    Id = x.ProductTypeId,
                    Name = x.ProductType.Name,
                    Code = x.ProductType.Code,
                    Description = x.ProductType.Description
                },

            }).ToList();

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener todos los productos.";
            logger.LogError(ex, msg);
            return Result.Failure<List<ProductGetAllResponse>>(Error.Unexpected(msg));
        }
    }
}
