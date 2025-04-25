using Final.Lab.Domain.Models;

namespace Final.Lab.Domain.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
}
