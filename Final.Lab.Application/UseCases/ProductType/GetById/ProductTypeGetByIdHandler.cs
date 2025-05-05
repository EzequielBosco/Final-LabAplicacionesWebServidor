using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.ProductType.GetById;

public class ProductTypeGetByIdHandler(IProductTypeService productTypeService) : 
                                       IRequestHandler<ProductTypeGetByIdQuery, Result<ProductTypeGetByIdResponse>>
{
    public async Task<Result<ProductTypeGetByIdResponse>> Handle(ProductTypeGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await productTypeService.GetById(request.Id);
        return result;
    }
}
