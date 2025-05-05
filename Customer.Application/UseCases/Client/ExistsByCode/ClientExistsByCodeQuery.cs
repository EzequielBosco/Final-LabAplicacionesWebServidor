using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.ExistsByCode;

public class ClientExistsByCodeQuery(string code) : IRequest<Result<bool>>
{
    public string Code { get; } = code;
}
