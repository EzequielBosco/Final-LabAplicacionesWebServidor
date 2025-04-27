using Final.Lab.Application.DTOs.Responses.Product;

namespace Final.Lab.Application.Services.Contracts;

public interface IProductService
{
    Task<ProductGetByIdResponse> GetById(int productId);
    Task<bool> ExistsByCode(string code);
}
