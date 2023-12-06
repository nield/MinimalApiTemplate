using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Integration.Tests.Mocks;

public class MockCurrentUserService : ICurrentUserService
{
    public string UserProfileId => "1";

    public string CorrelationId => "1";

    public string Token => "1";

    public string? UserId => "1";
}
