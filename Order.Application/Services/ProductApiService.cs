using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs.Requests.Product;
using Order.Application.DTOs.Responses.Product;
using Order.Application.Services.Contracts;
using Order.Domain.Extensions;
using Order.Domain.Results;
using Order.Domain.Results.Errors;
using Order.Domain.Results.Generic;
using System.Net.Http.Json;

namespace Order.Application.Services;

public class ProductApiService : IProductApiService
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    private readonly ILogger<ClientApiService> _logger;

    public ProductApiService(IConfiguration configuration, HttpClient httpClient, ILogger<ClientApiService> logger)
    {
        _url = configuration.GetProductApiURL();
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<List<ProductGetByIdsResponse>>> GetByIds(ProductGetByIdsRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_url}/api/Products/GetByIds", request);
        if (!response.IsSuccessStatusCode)
        {
            var msg = $"Error al obtener los producto de ProductApi.";
            _logger.LogError(msg);
            return Result.Failure<List<ProductGetByIdsResponse>>(Error.Unexpected(msg));
        }

        var result = await response.Content.ReadFromJsonAsync<List<ProductGetByIdsResponse>>();
        if (result is null)
        {
            var msg = $"Error al deserializar los productos de ProductApi.";
            _logger.LogError(msg);
            return Result.Failure<List<ProductGetByIdsResponse>>(Error.Unexpected(msg));
        }

        return result;
    }
}
