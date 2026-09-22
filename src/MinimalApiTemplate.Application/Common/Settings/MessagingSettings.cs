using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Application.Common.Settings;

[ExcludeFromCodeCoverage]
public class MessagingSettings
{
    public bool PublishEnabled { get; set; }
}
