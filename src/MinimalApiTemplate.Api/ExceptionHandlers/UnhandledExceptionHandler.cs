namespace MinimalApiTemplate.Api.ExceptionHandlers;

public class UnhandledExceptionHandler : BaseExceptionHandler<Exception>
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;

    public UnhandledExceptionHandler(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Note: Error logged in Mediator pipeline behavior
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>

    public override ProblemDetails GenerateProblemDetails(Exception exception)
    {
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
