namespace MinimalApiTemplate.Application.Common.Exceptions;

public class InvalidMappingException: ApplicationException
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