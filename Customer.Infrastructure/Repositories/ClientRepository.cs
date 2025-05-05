using Customer.Domain.Models;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Data;
using Customer.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Customer.Infrastructure.Repositories;

public class ClientRepository : RepositoryBase<Client>, IClientRepository
{
    public ClientRepository(DataContext context) : base(context) { }

    public override async Task<List<Client>> GetAll(bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .ToListAsync();
        return result;
    }

    public override async Task<List<Client>> GetAllBy(Expression<Func<Client, bool>> predicate, bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .Where(predicate)
                           .ToListAsync();
        return result;
    }

    public override async Task<Client?> GetById(int id, bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .FirstOrDefaultAsync(p => p.Id == id);
        return result;
    }

    public override async Task<Client?> GetBy(Expression<Func<Client, bool>> predicate, bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .FirstOrDefaultAsync(predicate);
        return result;
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
