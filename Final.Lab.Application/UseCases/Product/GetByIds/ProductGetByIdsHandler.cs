using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Repositories;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetByIds;

public class ProductGetByIdsHandler(IProductRepository productRepository, 
                                    ILogger<ProductGetByIdsHandler> logger) : 
                                    IRequestHandler<ProductGetByIdsCommand, Result<List<ProductGetByIdsResponse>>>
{
    public async Task<Result<List<ProductGetByIdsResponse>>> Handle(ProductGetByIdsCommand request, CancellationToken cancellationToken)
    {
		try
		{
			var products = await productRepository.GetAllBy(p => request.Ids.Contains(p.Id), includeDeleted: false);
            if (products is null || !products.Any())
            {
                return new List<ProductGetByIdsResponse>();
            }

            var result = products.Select(p => new ProductGetByIdsResponse
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
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
