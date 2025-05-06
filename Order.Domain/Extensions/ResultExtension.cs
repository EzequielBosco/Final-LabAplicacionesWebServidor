using Microsoft.AspNetCore.Mvc;
using Order.Domain.Results.Generic;
using System.Net;

namespace Order.Domain.Extensions;

public static class ResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            if (result.HttpStatusCode == HttpStatusCode.Created || result.HttpStatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(result.Value)
                {
                    StatusCode = (int)result.HttpStatusCode
                };
            }

            return new OkObjectResult(result.Value);
        }

        return result.HttpStatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundObjectResult(result.Errors),
            HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(result.Errors),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(result.Errors),
            HttpStatusCode.Forbidden => new ObjectResult(result.Errors)
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            },
            HttpStatusCode.InternalServerError => new ObjectResult(result.Errors)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            },
            _ => new ObjectResult(result.Errors)
            {
                StatusCode = (int)result.HttpStatusCode
            }
        };
    }
}
