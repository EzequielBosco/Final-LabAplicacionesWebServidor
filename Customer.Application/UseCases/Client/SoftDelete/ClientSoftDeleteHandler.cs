using Customer.Domain.Extensions;
using Customer.Domain.Repositories;
using Customer.Domain.Results;
using Customer.Domain.Results.Errors;
using Customer.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Customer.Application.UseCases.Client.SoftDelete;

public class ClientSoftDeleteHandler(IClientRepository clientRepository,
                                     ClientSoftDeleteValidation validations,
                                     ILogger<ClientSoftDeleteHandler> logger) : 
                                     IRequestHandler<ClientSoftDeleteCommand, Result<Domain.Results.Unit>>
{
    public async Task<Result<Domain.Results.Unit>> Handle(ClientSoftDeleteCommand command, CancellationToken cancellationToken)
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

            var result = await clientRepository.SoftDelete(client);
            if (!result)
            {
                var msg = $"Error al eliminar logicamente el cliente en la DB.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
            }

            var save = await clientRepository.Save();
            if (save == 0)
            {
                var msg = $"Error al guardar el cliente en la DB.";
                logger.LogError(msg);
                return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
            }

            return Result.Success(HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            var msg = "Error al eliminar logicamente el cliente.";
            logger.LogError(ex, msg);
            return Result.Failure<Domain.Results.Unit>(Error.Unexpected(msg));
        }
    }
}
