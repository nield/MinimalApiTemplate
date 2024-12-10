using Microsoft.AspNetCore.Diagnostics;

namespace MinimalApiTemplate.Api.Common.ExceptionHandlers;

public abstract class BaseExceptionHandler<TException, TProblem> : IExceptionHandler
    where TException : Exception
    where TProblem : ProblemDetails
{
    private readonly IProblemDetailsService _problemDetailsService;

    public abstract TProblem GenerateProblemDetails(TException exception);

    public abstract HttpStatusCode HttpStatusCode { get; }

    protected BaseExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public virtual async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is TException castedException)
        {
            var problemDetails = GenerateProblemDetails(castedException);

            await WriteErrorMessageToContext(
                httpContext, 
                HttpStatusCode, 
                problemDetails,
                castedException,
                cancellationToken);

            return true;
        }

        return false;
    }

    protected virtual async Task WriteErrorMessageToContext(
        HttpContext context,
        HttpStatusCode httpStatusCode,
        TProblem problemDetails,
        TException exception,
        CancellationToken cancellationToken)
    {
        context.Response.StatusCode = (int)httpStatusCode;

        await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = context,
            ProblemDetails = problemDetails,
        });
    }
}
