using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdHandler(IProductService productService) : 
                                   IRequestHandler<ProductGetByIdQuery, Result<ProductGetByIdResponse>>
{
    public async Task<Result<ProductGetByIdResponse>> Handle(ProductGetByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await productService.GetById(query.Id, query.IncludeDeleted);
        return result;
    }
}
