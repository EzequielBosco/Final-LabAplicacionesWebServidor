using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Final.Lab.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Final.Lab.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context) { }

    protected override IQueryable<Product> ApplyIncludes(IQueryable<Product> query)
    {
        return query.Include(p => p.ProductType);
    }

    public override async Task<List<Product>> GetAll(bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .ToListAsync();
        return result;
    }

    public override async Task<List<Product>> GetAllBy(Expression<Func<Product, bool>> predicate, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted).Where(predicate))
                           .ToListAsync();
        return result;
    }

    public override async Task<Product?> GetById(int id, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .FirstOrDefaultAsync(p => p.Id == id);
        return result;
    }

    public override async Task<Product?> GetBy(Expression<Func<Product, bool>> predicate, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .FirstOrDefaultAsync(predicate);
        return result;
    }
}
