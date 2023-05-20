using MinimalApiTemplate.Domain.Common;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class FakeEntity : BaseAuditableEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
