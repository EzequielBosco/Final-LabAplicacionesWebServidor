using Customer.Domain.Models;
using Customer.Domain.Repositories.Base;

namespace Customer.Domain.Repositories;

public interface IClientRepository : IRepositoryBase<Client>
{
    Task<int> Save();
}
