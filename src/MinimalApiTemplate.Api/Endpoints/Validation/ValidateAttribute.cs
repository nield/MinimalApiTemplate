using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Api.Endpoints;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{

}