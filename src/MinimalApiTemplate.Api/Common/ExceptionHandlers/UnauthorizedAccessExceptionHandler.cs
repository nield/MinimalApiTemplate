namespace MinimalApiTemplate.Api.Common.ExceptionHandlers;

public class UnauthorizedAccessExceptionHandler : BaseExceptionHandler<UnauthorizedAccessException, ProblemDetails>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.Unauthorized;

    public UnauthorizedAccessExceptionHandler(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    {
        
    }

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
