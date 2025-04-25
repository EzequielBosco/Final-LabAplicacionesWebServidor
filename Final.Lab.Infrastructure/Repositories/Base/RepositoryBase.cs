using Final.Lab.Domain.Repositories.Base;
using Final.Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Final.Lab.Infrastructure.Repositories.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
{
    protected readonly DataContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }
    
    public async Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsBy(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<bool> Create(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public Task<bool> Update(T entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(true);
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await GetById(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        return true;
    }
}
