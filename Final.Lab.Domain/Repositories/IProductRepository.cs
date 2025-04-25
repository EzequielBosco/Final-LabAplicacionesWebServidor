using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories.Base;

namespace Final.Lab.Domain.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<List<Product>> GetAllActive();
    Task<Product?> GetByIdActive(int id);
    Task<Product?> GetByCodeActive(string code);
}
