using System.Linq.Expressions;
using Final.Lab.Domain.Models.Base;

namespace Final.Lab.Domain.Repositories.Base;

public interface IRepositoryBase<T> where T : class, IEntity
{
    Task<List<T>> GetAll(bool? includeDeleted);
    Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate, bool? includeDeleted);
    Task<T?> GetById(int id, bool? includeDeleted);
    Task<T?> GetBy(Expression<Func<T, bool>> predicate, bool? includeDeleted);
    Task<bool> ExistsBy(Expression<Func<T, bool>> predicate, bool? includeDeleted);
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Patch(T entity, Action<T> updateAction);
    Task<bool> Delete(T entity);
    Task<bool> SoftDelete(T entity);
}
