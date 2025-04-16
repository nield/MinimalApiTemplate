using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using MinimalApiTemplate.Sdk.Interfaces;

namespace MinimalApiTemplate.Sdk.Services;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> GetTokenAsync()
    {
        var token = "";

        if (_httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue("Authorization", out StringValues values) ?? false)
        {
            token = values[0]?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase) ?? "";
        }

        return Task.FromResult(token);
    }
}
