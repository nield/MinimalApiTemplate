using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Application.Common.Settings;

[ExcludeFromCodeCoverage]
public class MassTransitSettings
{
    public bool PublishEnabled { get; set; }
}