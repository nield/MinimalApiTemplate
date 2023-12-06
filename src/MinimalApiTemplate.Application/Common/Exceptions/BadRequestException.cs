namespace MinimalApiTemplate.Application.Common.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    private BadRequestException()
    : base()
    {
    }
}
