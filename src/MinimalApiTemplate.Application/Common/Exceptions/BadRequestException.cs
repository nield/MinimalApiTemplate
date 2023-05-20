using System.Runtime.Serialization;

namespace MinimalApiTemplate.Application.Common.Exceptions;


[Serializable]
public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    private BadRequestException()
    : base()
    {
    }

    protected BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
        throw new BadRequestException();
    }
}
