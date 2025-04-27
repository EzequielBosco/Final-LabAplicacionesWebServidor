using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetAll;

public class ProductGetAllHandler(IProductRepository productRepository, 
                                  ILogger<ProductGetAllHandler> logger) : 
                                  IRequestHandler<ProductGetAllQuery, List<ProductGetAllResponse>>
{
    public async Task<List<ProductGetAllResponse>> Handle(ProductGetAllQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var products = await productRepository.GetAll(query.IncludeDeleted);

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
            throw new Exception(msg);
        }
    }
}
