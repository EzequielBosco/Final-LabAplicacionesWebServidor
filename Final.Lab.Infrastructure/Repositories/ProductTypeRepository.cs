using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Final.Lab.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Final.Lab.Infrastructure.Repositories;

public class ProductTypeRepository : RepositoryBase<ProductType>, IProductTypeRepository
{
    public ProductTypeRepository(DataContext context) : base(context) { }

    protected override IQueryable<ProductType> ApplyIncludes(IQueryable<ProductType> query)
    {
        return query.Include(p => p.Products);
    }

    public override async Task<List<ProductType>> GetAll(bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .ToListAsync();
        return result;
    }

    public override async Task<List<ProductType>> GetAllBy(Expression<Func<ProductType, bool>> predicate, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted).Where(predicate))
                           .ToListAsync();
        return result;
    }

    public override async Task<ProductType?> GetById(int id, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .FirstOrDefaultAsync(p => p.Id == id);
        return result;
    }

    public override async Task<ProductType?> GetBy(Expression<Func<ProductType, bool>> predicate, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .FirstOrDefaultAsync(predicate);
        return result;
    }
}
