using NotFoundException = MinimalApiTemplate.Application.Common.Exceptions.NotFoundException;

namespace MinimalApiTemplate.Api.Common.ExceptionHandlers;

public class NotFoundExceptionHandler : BaseExceptionHandler<NotFoundException, ProblemDetails>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public NotFoundExceptionHandler(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    {
        
    }

    public override ProblemDetails GenerateProblemDetails(NotFoundException exception)
    {
        return new ProblemDetails()
        {
            Status = (int)HttpStatusCode,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };
    }
}
