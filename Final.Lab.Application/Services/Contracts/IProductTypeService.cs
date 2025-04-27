using Final.Lab.Application.DTOs.Responses.ProductType;

namespace Final.Lab.Application.Services.Contracts;

public interface IProductTypeService
{
    Task<ProductTypeGetByIdResponse> GetById(int id);
}
