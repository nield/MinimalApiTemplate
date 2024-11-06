using System.Security.Claims;
using MinimalApiTemplate.Application.Common.Interfaces;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(Headers.UserProfileId);

    public string? UserProfileId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(Headers.UserProfileId);

    public string? CorrelationId
    {
        get 
        {
            var correlationId = _httpContextAccessor.HttpContext?.GetCorrelationId(allowEmpty: true);

            return correlationId;
        }
    }
        

    public string? Token =>
        _httpContextAccessor.HttpContext
            ?.Request?.Headers?.FirstOrDefault(x => x.Key == Headers.Authorization)
            .Value;
}