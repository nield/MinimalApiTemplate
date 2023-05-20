using System.Runtime.Serialization;

namespace MinimalApiTemplate.Application.Common.Exceptions;

[Serializable]
public class InvalidMappingException: Exception
{
    public InvalidMappingException(string message)
        : base(message)
    {
    }

    public InvalidMappingException(Type fromObject, Type toObject)
        : base($"Cannot map from {fromObject.Name} to {toObject.Name}.")
    {
    }


    protected InvalidMappingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}