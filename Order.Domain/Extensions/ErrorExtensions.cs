using FluentValidation.Results;
using Order.Domain.Results.Errors;
using System.Net;

namespace Order.Domain.Extensions;

public static class ErrorExtensions
{
    public static HttpStatusCode GetHttpStatusCode(this IEnumerable<Error> errors)
    {
        return errors.FirstOrDefault()?.ToStatusCode() ?? HttpStatusCode.BadRequest;
    }

    public static HttpStatusCode ToStatusCode(this Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Conflict => HttpStatusCode.Conflict,
            ErrorType.Authorization => HttpStatusCode.Unauthorized,
            ErrorType.Forbidden => HttpStatusCode.Forbidden,
            ErrorType.Unexpected => HttpStatusCode.InternalServerError,
            ErrorType.Validation => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.BadRequest
        };
    }

    public static string JoinMessages(this IEnumerable<Error> errors, string separator = " | ")
    {
        return string.Join(separator, errors.Select(e => e.Message));
    }

    public static string JoinMessages(this List<ValidationFailure> errors, string separator = " | ")
    {
        return string.Join(separator, errors.Select(e => e.ErrorMessage));
    }
}
