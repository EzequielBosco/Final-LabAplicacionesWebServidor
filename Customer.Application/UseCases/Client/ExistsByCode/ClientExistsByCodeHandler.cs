using Customer.Application.Services.Contracts;
using Customer.Domain.Extensions;
using Customer.Domain.Results;
using Customer.Domain.Results.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases.Client.ExistsByCode;

public class ClientExistsByCodeHandler(IClientService clientService, ILogger<ClientExistsByCodeHandler> logger) : IRequestHandler<ClientExistsByCodeQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(ClientExistsByCodeQuery query, CancellationToken cancellationToken)
    {
        var existsClient = await clientService.ExistsByCode(query.Code);
        if (!existsClient.IsSuccess)
        {
            logger.LogError("Error en Client ExistsByCode: {Errors}", existsClient.Errors.JoinMessages());
            return Result.Failure<bool>(existsClient.Errors);
        }

        return existsClient.Value;
    }
}
