using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.SoftDelete;

public class ClientSoftDeleteCommand(int id) : IRequest<Result<Domain.Results.Unit>>
{
    public int Id { get; } = id;
}