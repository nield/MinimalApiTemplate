using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Worker.Common;

public class CurrentUserService : ICurrentUserService
{
    public string? UserId => "SystemUser";
    public string? CorrelationId { get; } = null;
    public string? Token { get; } = null;
}