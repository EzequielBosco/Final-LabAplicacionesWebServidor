using Final.Lab.Application.DTOs.Responses.Product;
using Final.Lab.Domain.Results.Generic;

namespace Final.Lab.Application.Services.Contracts;

public interface IProductService
{
    Task<Result<ProductGetByIdResponse>> GetById(int productId, bool? includeDeleted = false);
    Task<Result<bool>> ExistsByCode(string code);
}
