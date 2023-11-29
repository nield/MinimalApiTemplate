using MinimalApiTemplate.Domain.Common;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class FakeEvent : BaseEvent
{
    public string Greeting { get; set; } = "";
}
