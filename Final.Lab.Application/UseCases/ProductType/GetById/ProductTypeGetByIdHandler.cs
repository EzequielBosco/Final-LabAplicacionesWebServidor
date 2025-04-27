using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Application.Services.Contracts;
using MediatR;

namespace Final.Lab.Application.UseCases.ProductType.GetById;

public class ProductTypeGetByIdHandler(IProductTypeService productTypeService) : 
                                       IRequestHandler<ProductTypeGetByIdQuery, ProductTypeGetByIdResponse>
{
    public async Task<ProductTypeGetByIdResponse> Handle(ProductTypeGetByIdQuery request, CancellationToken cancellationToken)
    {
        var productType = await productTypeService.GetById(request.Id);
        var result =  new ProductTypeGetByIdResponse
        {
            Id = productType.Id,
            Name = productType.Name,
            Code = productType.Code,
            Description = productType.Description,
            CreatedAt = productType.CreatedAt,
            UpdatedAt = productType.UpdatedAt,
            IsDeleted = productType.IsDeleted,
            DeletedAt = productType.DeletedAt,
        };

        return result;
    }
}
