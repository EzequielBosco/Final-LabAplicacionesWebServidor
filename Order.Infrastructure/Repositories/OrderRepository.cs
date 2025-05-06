using Microsoft.EntityFrameworkCore;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Domain.Models.Order>, IOrderRepository
{
    public OrderRepository(DataContext context) : base(context) { }

    protected override IQueryable<Domain.Models.Order> ApplyIncludes(IQueryable<Domain.Models.Order> query)
    {
        return query.Include(p => p.OrderItems);
    }

    public override async Task<List<Domain.Models.Order>> GetAll(bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .ToListAsync();
        return result;
    }

    public override async Task<List<Domain.Models.Order>> GetAllBy(Expression<Func<Domain.Models.Order, bool>> predicate, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted).Where(predicate))
                           .ToListAsync();
        return result;
    }

    public override async Task<Domain.Models.Order?> GetById(int id, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .FirstOrDefaultAsync(p => p.Id == id);
        return result;
    }

    public override async Task<Domain.Models.Order?> GetBy(Expression<Func<Domain.Models.Order, bool>> predicate, bool? includeDeleted)
    {
        var result = await ApplyIncludes(GetQueryable(includeDeleted))
                           .FirstOrDefaultAsync(predicate);
        return result;
    }
}
