using Customer.Domain.Extensions;
using Customer.Domain.Results.Errors;
using Customer.Domain.Results.Generic;
using System.Collections.Immutable;
using System.Net;

namespace Customer.Domain.Results;

public static class Result
{
    public static Result<T> Success<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK) => new Result<T>(value, statusCode);

    public static Result<T> Failure<T>(Error error) => new Result<T>(ImmutableArray.Create(error), error.ToStatusCode());

    public static Result<T> Failure<T>(ImmutableArray<Error> errors) => new Result<T>(errors, errors.GetHttpStatusCode());

    public static Result<T> Failure<T>(IEnumerable<Error> errors) => new Result<T>(errors.ToImmutableArray(), errors.GetHttpStatusCode());

    //  Unit
    public static Result<Unit> Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new Result<Unit>(Unit.Value, statusCode);

    public static Result<Unit> Failure(Error error) => new Result<Unit>(ImmutableArray.Create(error), error.ToStatusCode());

    public static Result<Unit> Failure(ImmutableArray<Error> errors) => new Result<Unit>(errors, errors.GetHttpStatusCode());

    public static Result<Unit> Failure(IEnumerable<Error> errors) => new Result<Unit>(errors.ToImmutableArray(), errors.GetHttpStatusCode());
}
