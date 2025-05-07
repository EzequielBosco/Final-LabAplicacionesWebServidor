using Customer.Application.DTOs.Responses.Client;
using Customer.Application.Services.Contracts;
using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.GetById;

public class ClientGetByIdHandler(IClientService clientService) : 
                                  IRequestHandler<ClientGetByIdQuery, Result<ClientGetByIdResponse>>
{
    public async Task<Result<ClientGetByIdResponse>> Handle(ClientGetByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await clientService.GetById(query.Id, query.IncludeDeleted);
        return result;
    }
}
