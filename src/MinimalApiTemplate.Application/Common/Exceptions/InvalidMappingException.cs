namespace MinimalApiTemplate.Application.Common.Exceptions;

public class InvalidMappingException : Exception
{
    public InvalidMappingException(string message)
        : base(message)
    {
    }

    public InvalidMappingException(Type fromObject, Type toObject)
        : base($"Cannot map from {fromObject.Name} to {toObject.Name}.")
    {
    }
}