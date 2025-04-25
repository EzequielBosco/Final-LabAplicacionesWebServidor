using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Final.Lab.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public IProductRepository ProductRepository { get; }
    public IProductTypeRepository ProductTypeRepository { get; }

    public UnitOfWork(DataContext context, IProductRepository productRepository, IProductTypeRepository productTypeRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        ProductTypeRepository = productTypeRepository;
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<List<Product>> GetByProductTypeId(int productTypeId)
    {
        var result = await _context.Products
                           .Where(p => p.ProductTypeId == productTypeId)
                           .ToListAsync();

        return result;
    }
}
