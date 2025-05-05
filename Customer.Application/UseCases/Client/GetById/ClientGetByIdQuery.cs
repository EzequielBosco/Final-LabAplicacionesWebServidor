using Customer.Application.DTOs.Responses.Client;
using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.GetById;

public class ClientGetByIdQuery(int id, bool? includeDeleted = false) : IRequest<Result<ClientGetByIdResponse>>
{
    public int Id { get; } = id;
    public bool? IncludeDeleted { get; } = includeDeleted;
}
