using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Final.Lab.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Final.Lab.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context) { }

    public async Task<List<Product>> GetAllActive()
    {
        var products = await _context.Products
                             .Include(p => p.ProductType)
                             .Where(p => p.IsDeleted == false)
                             .ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdActive(int id)
    {
        var result = await _context.Products
                           .Where(p => p.IsDeleted == false)
                           .FirstOrDefaultAsync(p => p.Id == id);

        return result;
    }

    public async Task<Product?> GetByCodeActive(string code)
    {
        var result = await _context.Products
                           .Include(p => p.ProductType)
                           .FirstOrDefaultAsync(p => p.Code == code && 
                                                     p.IsDeleted == false);

        return result;
    }
}
