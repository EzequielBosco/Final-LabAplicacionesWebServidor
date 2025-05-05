using Final.Lab.Application.DTOs.Responses.ProductType;
using Final.Lab.Domain.Results.Generic;

namespace Final.Lab.Application.Services.Contracts;

public interface IProductTypeService
{
    Task<Result<ProductTypeGetByIdResponse>> GetById(int id);
}
