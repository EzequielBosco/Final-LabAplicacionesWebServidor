using System.Linq.Expressions;

namespace Final.Lab.Domain.Repositories.Base;

public interface IRepositoryBase<T> where T : class, IEntity
{
    Task<List<T>> GetAll();
    Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate);
    Task<T?> GetById(int id);
    Task<T?> GetBy(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsBy(Expression<Func<T, bool>> predicate);
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
}
