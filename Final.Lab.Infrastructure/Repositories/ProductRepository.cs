using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Final.Lab.Infrastructure.Repositories;

public class ProductRepository(DataContext _dataContext) : IProductRepository
{
    public async Task<List<Product>> GetAll()
    {
        var products = await _dataContext.Products
                             .Include(p => p.ProductType)
                             .Where(p => p.IsDeleted == false)
                             .ToListAsync();

        return products;
    }
}
