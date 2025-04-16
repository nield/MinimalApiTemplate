using Microsoft.Extensions.Caching.Memory;
using MinimalApiTemplate.Sdk.Interfaces;

namespace MinimalApiTemplate.Sdk.Services;

public class TokenService : ITokenService
{
    private const string CacheKey = "BearerToken";

    private readonly IMemoryCache _cache;

    public TokenService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<string> GetTokenAsync()
    {
        if (_cache.TryGetValue<string>(CacheKey, out string? cachedToken))
        {
            return cachedToken ?? "";
        }

        var newToken = await GetNewToken();

        _cache.Set<string>(CacheKey, newToken, TimeSpan.FromMinutes(10));

        return newToken; 
    }

    private Task<string> GetNewToken()
    {
        // impement fetching new token from IDP

        return Task.FromResult("xyz");
    }
}
