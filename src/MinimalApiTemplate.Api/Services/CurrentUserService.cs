using System.Security.Claims;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserProfileId");

    public string? UserProfileId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserProfileId");

    public string? CorrelationId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue("CorrelationId");

    public string? Token =>
        _httpContextAccessor.HttpContext
            ?.Request?.Headers?.FirstOrDefault(x => x.Key == "Authorization")
            .Value;
}