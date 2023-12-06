using NotFoundException = MinimalApiTemplate.Application.Common.Exceptions.NotFoundException;

namespace MinimalApiTemplate.Api.ExceptionHandlers;

public class NotFoundExceptionHandler : BaseExceptionHandler<NotFoundException>
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public override ProblemDetails GenerateProblemDetails(NotFoundException exception)
    {
        return new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };
    }
}
