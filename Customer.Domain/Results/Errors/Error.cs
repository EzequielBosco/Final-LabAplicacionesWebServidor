using System.Text.Json.Serialization;

namespace Customer.Domain.Results.Errors;

public sealed class Error
{
    public string Message { get; }
    [JsonIgnore]
    public ErrorType Type { get; }

    private Error(string message, ErrorType type = ErrorType.Validation)
    {
        Message = message;
        Type = type;
    }

    public static readonly Error None = new Error(string.Empty, ErrorType.None);
    public static Error NotFound(string message) => new(message, ErrorType.NotFound);
    public static Error Exists(string message) => new(message, ErrorType.Conflict);
    public static Error Validation(string message) => new(message, ErrorType.Validation);
    public static Error Autorization(string message) => new(message, ErrorType.Authorization);
    public static Error Unexpected(string message) => new(message, ErrorType.Unexpected);
}
