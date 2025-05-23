﻿using Customer.Application.DTOs.Responses.Client;
using Customer.Domain.Results.Generic;

namespace Customer.Application.Services.Contracts;

public interface IClientService
{
    Task<Result<ClientGetByIdResponse>> GetById(int clientId, bool? includeDeleted = false);
    Task<Result<bool>> ExistsByCode(string code);
}
