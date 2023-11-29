using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public class UnhandledExceptionHandler : BaseExceptionHandler<Exception>
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<UnhandledExceptionHandler> _logger;

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;

    public UnhandledExceptionHandler(IWebHostEnvironment webHostEnvironment,
        ILogger<UnhandledExceptionHandler> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    public override ProblemDetails GenerateProblemDetails(Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception");

        var errorDetail = "An error occurred while processing your request.";

        if (!_webHostEnvironment.IsProduction() && !_webHostEnvironment.IsStaging())
        {
            errorDetail = exception.GetFullErrorMessage();
        }

        return new ProblemDetails
        {
            Status = (int)HttpStatusCode,
            Title = "Internal Server Error",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = errorDetail
        };
    }
}
