using Final.Lab.Application.Services.Contracts;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.ExistsByCode;

public class ProductExistsByCodeHandler(IProductService productService) : IRequestHandler<ProductExistsByCodeQuery, bool>
{
    public async Task<bool> Handle(ProductExistsByCodeQuery query, CancellationToken cancellationToken)
    {
        var existsProduct = await productService.ExistsByCode(query.Code);
        return existsProduct;
    }
}
