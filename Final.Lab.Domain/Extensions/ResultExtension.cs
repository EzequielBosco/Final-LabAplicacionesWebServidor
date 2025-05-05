using Final.Lab.Domain.Results.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Final.Lab.Domain.Extensions;

public static class ResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            if (result.HttpStatusCode == HttpStatusCode.Created)
            {
                return new ObjectResult(result.Value)
                {
                    StatusCode = (int)HttpStatusCode.Created
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
