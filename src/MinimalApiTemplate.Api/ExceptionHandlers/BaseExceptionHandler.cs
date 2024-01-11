using Microsoft.AspNetCore.Diagnostics;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public abstract class BaseExceptionHandler<TException, TProblem> : IExceptionHandler 
    where TException : Exception
    where TProblem : ProblemDetails
{
    public abstract TProblem GenerateProblemDetails(TException exception);

    public abstract HttpStatusCode HttpStatusCode { get; }

    public virtual async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is TException castedException)
        {
            var problemDetails = GenerateProblemDetails(castedException);

            await WriteErrorMessageToContext(httpContext, HttpStatusCode, problemDetails, cancellationToken);

            return true;
        }

        return false;
    }

    protected virtual async Task WriteErrorMessageToContext(
        HttpContext context,
        HttpStatusCode httpStatusCode,
        TProblem problemDetails,
        CancellationToken cancellationToken)
    {
        context.Response.StatusCode = (int)httpStatusCode;

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}
