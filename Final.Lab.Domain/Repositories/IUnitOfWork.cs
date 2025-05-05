using Final.Lab.Domain.Models;

namespace Final.Lab.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IProductTypeRepository ProductTypeRepository { get; }
    Task<List<Product>> GetProductByProductTypeId(int productTypeId, bool? includeDeleted = false);
    Task<int> Save();
}
