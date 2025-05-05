using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Results;
using Final.Lab.Domain.Results.Errors;
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
            var msg = $"Error en ExistsByCode: {existsProduct.Errors}";
            logger.LogError(msg);
            return Result.Failure<bool>(Error.Unexpected(msg));
        }

        return existsProduct.Value;
    }
}
