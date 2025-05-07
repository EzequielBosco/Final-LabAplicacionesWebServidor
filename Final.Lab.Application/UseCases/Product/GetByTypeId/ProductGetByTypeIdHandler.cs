using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetByProductTypeId;

public class ProductGetByTypeIdHandler(IUnitOfWork unitOfWork, 
                                       IProductTypeService productTypeService,
                                       ILogger<ProductGetByTypeIdHandler> logger) : 
                                       IRequestHandler<ProductGetByTypeIdQuery, Result<List<ProductGetByTypeIdResponse>>>
{
    public async Task<Result<List<ProductGetByTypeIdResponse>>> Handle(ProductGetByTypeIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var existsProductType = await productTypeService.GetById(query.ProductTypeId);
            if (!existsProductType.IsSuccess)
            {
                logger.LogError("Error en ProductType GetById: {Errors}", existsProductType.Errors.JoinMessages());
                return Result.Failure<List<ProductGetByTypeIdResponse>>(existsProductType.Errors);
            }
            
            if (existsProductType.Value == null)
            {
                var msg = $"El tipo de producto con id {query.ProductTypeId} no existe.";
                logger.LogError(msg);
                return Result.Failure<List<ProductGetByTypeIdResponse>>(Error.NotFound(msg));
            }

            var products = await unitOfWork.GetProductByProductTypeId(query.ProductTypeId, query.IncludeDeleted);
            if (products == null || !products.Any())
            {
                logger.LogWarning($"No se encontraron productos para el tipo de producto con id {query.ProductTypeId}.");
            }

            var result = products.Select(p => new ProductGetByTypeIdResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Code = p.Code,
                Stock = p.Stock,
                UnitPrice = p.UnitPrice,
                IsDeleted = p.IsDeleted,
                ProductTypeId = p.ProductTypeId,
                ProductTypeName = p.ProductType.Name,
            }).ToList();

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener los productos por su tipo.";
            logger.LogError(ex, msg);
            return Result.Failure<List<ProductGetByTypeIdResponse>>(Error.Unexpected(msg));
        }
    }
}
