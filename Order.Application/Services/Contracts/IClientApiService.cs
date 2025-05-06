using Order.Application.DTOs.Responses.Client;
using Order.Domain.Results.Generic;

namespace Order.Application.Services.Contracts;

public interface IClientApiService
{
    Task<Result<ClientGetByIdResponse>> GetById(int id);
}
