using FluentValidation.Results;

namespace MinimalApiTemplate.Application.Common.Exceptions;

public class DataValidationFailureException : ApplicationException
{
    public DataValidationFailureException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public DataValidationFailureException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
