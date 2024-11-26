using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Api.Common.Validation;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{

}