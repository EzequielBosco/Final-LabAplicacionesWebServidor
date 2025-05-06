using Microsoft.Extensions.Configuration;

namespace Order.Domain.Extensions;

public static class ConfigurationExtensions
{
    public static string GetCustomerApiURL(this IConfiguration configuration)
    {
        var customerApiUrl = configuration.GetValue<string>("CustomerApiUrl");
        if (string.IsNullOrEmpty(customerApiUrl))
        {
            throw new ArgumentNullException(nameof(customerApiUrl), "Customer API URL sin configurar.");
        }
        return customerApiUrl;
    }

    public static string GetProductApiURL(this IConfiguration configuration)
    {
        var customerApiUrl = configuration.GetValue<string>("ProductApiUrl");
        if (string.IsNullOrEmpty(customerApiUrl))
        {
            throw new ArgumentNullException(nameof(customerApiUrl), "Product API URL sin configurar.");
        }
        return customerApiUrl;
    }
}
