using Customer.Application.DTOs.Requests.Client;
using Customer.Application.DTOs.Responses.Client;
using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.Create;

public class ClientCreateCommand(ClientCreateRequest request) : IRequest<Result<ClientCreateResponse>>
{
    public string FirstName { get; } = request.FirstName;
    public string? LastName { get; } = request.LastName;
    public string Email { get; } = request.Email;
    public string? Phone { get; } = request.Phone;
    public string Address { get; } = request.Address;
    public DateTime DateOfBirth { get; } = request.DateOfBirth;
}
