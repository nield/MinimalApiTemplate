using MinimalApiTemplate.Application.Common.Exceptions;

namespace MinimalApiTemplate.Api.Common.ExceptionHandlers;

public class ForbiddenAccessExceptionHandler : BaseExceptionHandler<ForbiddenAccessException, ProblemDetails>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;

    public ForbiddenAccessExceptionHandler(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    {
        
    }

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
