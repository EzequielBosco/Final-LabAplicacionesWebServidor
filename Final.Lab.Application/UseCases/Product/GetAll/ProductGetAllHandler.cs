using Final.Lab.Application.DTOs.Responses;
using Final.Lab.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.GetAll;

public class ProductGetAllHandler(IProductRepository productRepository, ILogger<ProductGetAllHandler> logger) : IRequestHandler<ProductGetAllQuery, List<ProductResponse>>
{
    public async Task<List<ProductResponse>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await productRepository.GetAll();

            var result = products.Select(x => new ProductResponse
            {
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                Stock = x.Stock,
                ProductTypeId = x.ProductTypeId
            }).ToList();

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener todos los productos.";
            logger.LogError(ex, msg);
            throw new Exception(msg, ex);
        }
    }
}
