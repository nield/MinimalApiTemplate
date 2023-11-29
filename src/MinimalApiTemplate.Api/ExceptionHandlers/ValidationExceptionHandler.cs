using System.Net;
using Microsoft.AspNetCore.Mvc;
using MinimalApiTemplate.Application.Common.Exceptions;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public class ValidationExceptionHandler : BaseExceptionHandler<ValidationException>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public override ProblemDetails GenerateProblemDetails(ValidationException exception)
    {
        return new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
    }
}
