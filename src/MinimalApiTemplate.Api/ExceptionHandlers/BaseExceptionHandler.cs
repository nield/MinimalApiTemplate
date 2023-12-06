using Microsoft.AspNetCore.Diagnostics;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public abstract class BaseExceptionHandler<TException> : IExceptionHandler where TException : Exception
{
    public abstract ProblemDetails GenerateProblemDetails(TException exception);

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

    protected static async Task WriteErrorMessageToContext<T>(
        HttpContext context,
        HttpStatusCode httpStatusCode,
        T problemDetails,
        CancellationToken cancellationToken) where T : ProblemDetails
    {
        context.Response.StatusCode = (int)httpStatusCode;

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}
