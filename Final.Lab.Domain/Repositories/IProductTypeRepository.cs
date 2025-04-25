using Final.Lab.Domain.Models;

namespace Final.Lab.Domain.Repositories;

public interface IProductTypeRepository
{
    Task<List<ProductType>> GetAllActive();
    Task<ProductType?> GetByCodeActive(string code);
}
