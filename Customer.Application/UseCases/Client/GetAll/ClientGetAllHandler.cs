using Customer.Application.DTOs.Responses.Client;
using Customer.Domain.Repositories;
using Customer.Domain.Results;
using Customer.Domain.Results.Errors;
using Customer.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases.Client.GetAll;

public class ClientGetAllHandler(IClientRepository clientRepository,
                                 ILogger<ClientGetAllHandler> logger) : 
                                 IRequestHandler<ClientGetAllQuery, Result<List<ClientGetAllResponse>>>
{
    public async Task<Result<List<ClientGetAllResponse>>> Handle(ClientGetAllQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var clients = await clientRepository.GetAll(query.IncludeDeleted);
            if (clients == null || !clients.Any())
            {
                logger.LogWarning("No se encontraron productos.");
            }

            var result = clients.Select(x => new ClientGetAllResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth
            }).ToList();

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener todos los clientes.";
            logger.LogError(ex, msg);
            return Result.Failure<List<ClientGetAllResponse>>(Error.Unexpected(msg));
        }
    }
}
