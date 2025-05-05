using Customer.Application.DTOs.Requests.Client;
using Customer.Domain.Results.Generic;
using MediatR;

namespace Customer.Application.UseCases.Client.Update;

public class ClientUpdateCommand(int id, ClientUpdateRequest request) : IRequest<Result<Domain.Results.Unit>>
{
    public int Id { get; } = id;
    public string? FirstName { get; } = request.FirstName;
    public string? LastName { get; } = request.LastName;
    public string? Email { get; } = request.Email;
    public string? Phone { get; } = request.Phone;
    public string? Address { get; } = request.Address;
}
