using Customer.Application.DTOs.Responses.Client;
using Customer.Domain.Extensions;
using Customer.Domain.Repositories;
using Customer.Domain.Results;
using Customer.Domain.Results.Errors;
using Customer.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Customer.Application.UseCases.Client.Create;

public class ClientCreateHandler(IClientRepository clientRepository, 
                                 ClientCreateValidation validations,
                                 ILogger<ClientCreateHandler> logger) : 
                                 IRequestHandler<ClientCreateCommand, Result<ClientCreateResponse>>
{
    public async Task<Result<ClientCreateResponse>> Handle(ClientCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<ClientCreateResponse>(Error.Validation(errors));
            }

            var client = new Domain.Models.Client
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                DateOfBirth = command.DateOfBirth,
                Email = command.Email,
                Phone = command.Phone,
                Address = command.Address,
                Code = ClientExtensions.GenerateCode(),
                CreatedAt = DateTime.UtcNow
            };

            var create = await clientRepository.Create(client);
            if (!create)
            {
                var msg = $"Error al crear el cliente en la DB.";
                logger.LogError(msg);
                return Result.Failure<ClientCreateResponse>(Error.Unexpected(msg));
            }

            var save = await clientRepository.Save();
            if (save == 0)
            {
                var msg = $"Error al guardar el cliente en la DB.";
                logger.LogError(msg);
                return Result.Failure<ClientCreateResponse>(Error.Unexpected(msg));
            }

            var result = new ClientCreateResponse
            {
                Id = client.Id,
            };

            return Result.Success(result, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            var msg = "Error al crear el cliente.";
            logger.LogError(ex, msg);
            return Result.Failure<ClientCreateResponse>(Error.Unexpected(msg));
        }
    }
}
