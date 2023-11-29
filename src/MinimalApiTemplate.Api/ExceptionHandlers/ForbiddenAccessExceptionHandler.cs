using System.Net;
using Microsoft.AspNetCore.Mvc;
using MinimalApiTemplate.Application.Common.Exceptions;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public class ForbiddenAccessExceptionHandler : BaseExceptionHandler<ForbiddenAccessException>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;

    public override ProblemDetails GenerateProblemDetails(ForbiddenAccessException exception)
    {
        return new ProblemDetails
        {
            Status = (int)HttpStatusCode,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };
    }
}
