using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Api.Endpoints.Common;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{

}