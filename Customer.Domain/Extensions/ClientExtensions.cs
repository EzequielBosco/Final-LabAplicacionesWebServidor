namespace Customer.Domain.Extensions;

public static class ClientExtensions
{
    public static string GenerateCode()
    {
        return $"CL-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
    }
}
