using Final.Lab.Domain.Models.Base;
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

    protected IQueryable<T> GetQueryable(bool? includeDeleted)
    {
        if (includeDeleted.HasValue && includeDeleted == true)
        {
            return _dbSet;
        }

        return _dbSet.Where(x => !x.IsDeleted);
    }

    protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> query)
    {
        return query;
    }

    public virtual async Task<List<T>> GetAll(bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .ToListAsync();
        return result;
    }
    
    public virtual async Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate, bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .Where(predicate)
                           .ToListAsync();
        return result;
    }

    public virtual async Task<T?> GetById(int id, bool? includeDeleted = false)
    {
        var result = await GetQueryable(includeDeleted)
                           .FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public virtual async Task<T?> GetBy(Expression<Func<T, bool>> predicate, bool? includeDeleted)
    {
        var result = await GetQueryable(includeDeleted)
                           .FirstOrDefaultAsync(predicate);
        return result;
    }

    public async Task<bool> ExistsBy(Expression<Func<T, bool>> predicate, bool? includeDeleted)
    {
        var result =  await GetQueryable(includeDeleted)
                            .AnyAsync(predicate);
        return result;
    }

    public async Task<bool> Create(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public async Task<bool> Update(T entity)
    {
        _dbSet.Update(entity);
        return await Task.FromResult(true);
    }

    public async Task<bool> Patch(T entity, Action<T> updateAction)
    {
        updateAction(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(true);
    }

    public async Task<bool> SoftDelete(T entity)
    {
        _dbSet.Update(entity);
        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
        return await Task.FromResult(true);
    }
}
