using Customer.Application.DTOs.Responses.Client;
using Customer.Application.Services.Contracts;
using Customer.Application.UseCases.Client.ExistsByCode;
using Customer.Application.UseCases.Client.GetById;
using Customer.Domain.Extensions;
using Customer.Domain.Repositories;
using Customer.Domain.Results;
using Customer.Domain.Results.Errors;
using Customer.Domain.Results.Generic;
using Microsoft.Extensions.Logging;

namespace Customer.Application.Services;

public class ClientService(IClientRepository clientRepository,
                           ClientGetByIdValidation getByIdValidations,
                           ClientExistsByCodeValidation existsByCodeValidations,
                           ILogger<ClientService> logger) : IClientService
{
    public async Task<Result<bool>> ExistsByCode(string code)
    {
        try
        {
            var query = new ClientExistsByCodeQuery(code);
            var validationResult = await existsByCodeValidations.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => Error.Validation(e.ErrorMessage));
                return Result.Failure<bool>(errors);
            }

            var existsClient = await clientRepository.ExistsBy(x => x.Code == query.Code);
            if (existsClient)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener existencia del cliente por código.";
            logger.LogError(ex, msg);
            return Result.Failure<bool>(Error.Unexpected(msg));
        }
    }

    public async Task<Result<ClientGetByIdResponse>> GetById(int clientId)
    {
        try
        {
            var query = new ClientGetByIdQuery(clientId);
            var validationResult = await getByIdValidations.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.JoinMessages();
                logger.LogError("Errores de validación: {Errors}", errors);
                return Result.Failure<ClientGetByIdResponse>(Error.Validation(errors));
            }

            var client = await clientRepository.GetById(query.Id, query.IncludeDeleted);
            if (client == null)
            {
                var msg = $"El cliente con id {query.Id} no existe.";
                logger.LogError(msg);
                return Result.Failure<ClientGetByIdResponse>(Error.NotFound(msg));
            }

            var result = new ClientGetByIdResponse
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Code = client.Code,
                Phone = client.Phone,
                Email = client.Email,
                Address = client.Address,
                DateOfBirth = client.DateOfBirth,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt,
                IsDeleted = client.IsDeleted,
                DeletedAt = client.DeletedAt
            };

            return result;
        }
        catch (Exception ex)
        {
            var msg = "Error al obtener el cliente por Id.";
            logger.LogError(ex, msg);
            return Result.Failure<ClientGetByIdResponse>(Error.Unexpected(msg));
        }
    }
}
