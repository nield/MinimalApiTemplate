using System.Security.Claims;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => 
        _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? CorrelationId =>
        _httpContextAccessor.HttpContext?.GetCorrelationId(allowEmpty: true);

    public string? Token =>
        _httpContextAccessor.HttpContext
            ?.Request.Headers.FirstOrDefault(x => x.Key == Headers.Authorization)
            .Value;
}