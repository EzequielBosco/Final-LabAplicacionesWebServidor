using Customer.Application.DTOs.Responses.Client;
using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.GetAll;

public class ClientGetAllQuery(bool? includeDeleted) : IRequest<Result<List<ClientGetAllResponse>>>
{
    public bool? IncludeDeleted { get; } = includeDeleted;
}
