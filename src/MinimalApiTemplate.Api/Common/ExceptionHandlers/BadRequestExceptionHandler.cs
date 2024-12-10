using MinimalApiTemplate.Application.Common.Exceptions;

namespace MinimalApiTemplate.Api.Common.ExceptionHandlers;

public class BadRequestExceptionHandler : BaseExceptionHandler<BadRequestException, ProblemDetails>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public BadRequestExceptionHandler(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    {
        
    }

    public override ProblemDetails GenerateProblemDetails(BadRequestException exception)
    {
        return new ProblemDetails()
        {
            Status = (int)HttpStatusCode,
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Detail = exception.Message
        };
    }
}
