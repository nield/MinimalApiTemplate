﻿using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public class UnauthorizedAccessExceptionHandler : BaseExceptionHandler<UnauthorizedAccessException>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.Unauthorized;

    public override ProblemDetails GenerateProblemDetails(UnauthorizedAccessException exception)
    {
        return new ProblemDetails
        {
            Status = (int)HttpStatusCode,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };
    }
}