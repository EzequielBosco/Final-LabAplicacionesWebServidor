using Customer.Domain.Extensions;
using Customer.Domain.Repositories;
using Customer.Domain.Results;
using Customer.Domain.Results.Errors;
using Customer.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases.Client.Update;

public class ClientUpdateHandler(IClientRepository clientRepository,
                                 ClientUpdateValidation validations,
                                 ILogger<ClientUpdateHandler> logger) : 
                                 IRequestHandler<ClientUpdateCommand, Result<Domain.Results.Unit>>
{
    public async Task<Result<Domain.Results.Unit>> Handle(ClientUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validations.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<Domain.Results.Unit>(Error.Validation(errors));
            }

            var client = await clientRepository.GetById(command.Id);
            if (client == null)
            {
                var msg = $"Error al obtener el cliente con Id: {command.Id}.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.NotFound(msg));
            }

            client.FirstName = command.FirstName ?? client.FirstName;
            client.LastName = command.LastName ?? client.LastName;
            client.Email = command.Email ?? client.Email;
            client.Phone = command.Phone ?? client.Phone;
            client.Address = command.Address ?? client.Address;
            client.UpdatedAt = DateTime.UtcNow;
            client.DateOfBirth = command.DateOfBirth ?? client.DateOfBirth;

            var save = await clientRepository.Save();
            if (save == 0)
            {
                var msg = "Error al guardar los cambios en la DB.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            var msg = "Error al actualizar el cliente.";
            logger.LogError(ex, msg);
            return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
        }
    }
}
