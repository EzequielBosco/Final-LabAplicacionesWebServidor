using Order.Domain.Results.Errors;
using System.Collections.Immutable;
using System.Net;

namespace Order.Domain.Results.Generic;

public struct Result<T>
{
    public readonly T Value;
    public readonly ImmutableArray<Error> Errors;
    public readonly HttpStatusCode HttpStatusCode;
    public bool IsSuccess => Errors.Length == 0;

    public static implicit operator Result<T>(T value) => new Result<T>(value, HttpStatusCode.OK);
    public static implicit operator Result<T>(ImmutableArray<Error> errors) => new Result<T>(errors, HttpStatusCode.NotFound);

    public Result(T value, HttpStatusCode statusCode)
    {
        Value = value;
        Errors = ImmutableArray<Error>.Empty;
        HttpStatusCode = statusCode;
    }

    public Result(ImmutableArray<Error> errors, HttpStatusCode statusCode)
    {
        if (errors.Length == 0)
        {
            throw new InvalidOperationException("Debe contener al menos un error.");
        }

        Value = default(T);
        Errors = errors;
        HttpStatusCode = statusCode;
    }
}
