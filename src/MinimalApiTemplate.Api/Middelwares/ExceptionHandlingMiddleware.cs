using MinimalApiTemplate.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MinimalApiTemplate.Api.Filters;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly IDictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ExceptionHandlingMiddleware(IWebHostEnvironment webHostEnvironment)
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                { typeof(BadRequestException), HandleBadRequestException },
            };

        _webHostEnvironment = webHostEnvironment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        Type type = exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            await _exceptionHandlers[type].Invoke(context, exception);
            return;
        }

        await HandleUnhandledException(context, exception);
    }

    private async Task HandleValidationException(HttpContext context, Exception exception)
    {
        var validatoinException = (ValidationException)exception;

        var details = new ValidationProblemDetails(validatoinException.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        await WriteErrorMessageToContext(context, HttpStatusCode.BadRequest, details);

   }

    private async Task HandleUnhandledException(HttpContext context, Exception exception)
    {
        var errorDetail = "An error occurred while processing your request.";

        if (!_webHostEnvironment.IsProduction() && !_webHostEnvironment.IsStaging())
        {
            errorDetail = exception.GetFullErrorMessage();
        }

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = errorDetail
        };

        await WriteErrorMessageToContext(context, HttpStatusCode.InternalServerError, details);
    }

    private async Task HandleNotFoundException(HttpContext context, Exception exception)
    {
        var notFoundexception = (NotFoundException)exception;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = notFoundexception.Message
        };

        await WriteErrorMessageToContext(context, HttpStatusCode.NotFound, details);
    }

    private async Task HandleBadRequestException(HttpContext context, Exception exception)
    {
        var badRequestException = (BadRequestException)exception;

        var details = new ProblemDetails()
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Detail = badRequestException.Message
        };

        await WriteErrorMessageToContext(context, HttpStatusCode.BadRequest, details);
    }

    private async Task HandleUnauthorizedAccessException(HttpContext context, Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        await WriteErrorMessageToContext(context, HttpStatusCode.Unauthorized, details);
    }

    private async Task HandleForbiddenAccessException(HttpContext context, Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        await WriteErrorMessageToContext(context, HttpStatusCode.Forbidden, details);
    }
    
    private static async Task WriteErrorMessageToContext<T>(HttpContext context, 
        HttpStatusCode httpStatusCode, T problemDetails) where T : ProblemDetails
    {
        context.Response.StatusCode = (int)httpStatusCode;

        await context.Response.WriteAsJsonAsync<T>(problemDetails);
    }
}
