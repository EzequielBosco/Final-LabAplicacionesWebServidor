using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Final.Lab.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    protected readonly DbSet<Product> _dbSet;
    public IProductRepository ProductRepository { get; }
    public IProductTypeRepository ProductTypeRepository { get; }

    public UnitOfWork(DataContext context, IProductRepository productRepository, IProductTypeRepository productTypeRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        ProductTypeRepository = productTypeRepository;
        _dbSet = _context.Set<Product>();
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<List<Product>> GetProductByProductTypeId(int productTypeId, bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .Include(p => p.ProductType)
                           .Where(p => p.ProductTypeId == productTypeId)
                           .ToListAsync();

        return result;
    }

    protected IQueryable<Product> GetQueryable(bool? includeDeleted)
    {
        if (includeDeleted.HasValue && includeDeleted == true)
        {
            return _dbSet;
        }

        return _dbSet.Where(x => !x.IsDeleted);
    }
}
