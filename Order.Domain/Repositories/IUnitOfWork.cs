namespace Order.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> Save();
}
