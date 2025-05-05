using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Extensions;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.ExistsByCode;

public class ProductExistsByCodeHandler(IProductService productService, ILogger<ProductExistsByCodeHandler> logger) : IRequestHandler<ProductExistsByCodeQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(ProductExistsByCodeQuery query, CancellationToken cancellationToken)
    {
        var existsProduct = await productService.ExistsByCode(query.Code);
        if (!existsProduct.IsSuccess)
        {
            logger.LogError("Error en Product ExistsByCode: {Errors}", existsProduct.Errors.JoinMessages());
            return Result.Failure<bool>(existsProduct.Errors);
        }

        return existsProduct.Value;
    }
}
