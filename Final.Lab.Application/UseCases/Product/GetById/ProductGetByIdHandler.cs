using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.Services.Contracts;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdHandler(IProductService productService) : 
                                   IRequestHandler<ProductGetByIdQuery, ProductGetByIdResponse>
{
    public async Task<ProductGetByIdResponse> Handle(ProductGetByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await productService.GetById(query.Id);
        return result;
    }
}
