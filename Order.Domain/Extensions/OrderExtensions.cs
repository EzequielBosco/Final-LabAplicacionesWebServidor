namespace Order.Domain.Extensions;

public static class OrderExtensions
{
    public static string GenerateCode()
    {
        return $"CL-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
    }
}
