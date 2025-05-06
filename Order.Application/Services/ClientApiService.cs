using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs.Responses.Client;
using Order.Application.Services.Contracts;
using Order.Domain.Extensions;
using Order.Domain.Results;
using Order.Domain.Results.Errors;
using Order.Domain.Results.Generic;
using System.Net.Http.Json;

namespace Order.Application.Services;

public class ClientApiService : IClientApiService
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    private readonly ILogger<ClientApiService> _logger;

    public ClientApiService(IConfiguration configuration, HttpClient httpClient, ILogger<ClientApiService> logger)
    {
        _url = configuration.GetCustomerApiURL();
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<ClientGetByIdResponse>> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"{_url}/api/Clients/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var msg = $"Error al obtener el cliente con id {id} de CustomerApi.";
            _logger.LogError(msg);
            return Result.Failure<ClientGetByIdResponse>(Error.Unexpected(msg));
        }

        var result = await response.Content.ReadFromJsonAsync<ClientGetByIdResponse>();
        if (result is null)
        {
            var msg = $"Error al deserializar el cliente con id {id} de CustomerApi.";
            _logger.LogError(msg);
            return Result.Failure<ClientGetByIdResponse>(Error.Unexpected(msg));
        }

        return result;
    }
}
