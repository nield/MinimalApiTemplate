#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.Caching.Distributed;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class CacheExtensioncs
{
    private static readonly DistributedCacheEntryOptions DefaultCacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
    };

    public static async Task<T?> GetAsync<T>(this IDistributedCache cache,
        string key,
        CancellationToken cancellationToken = default) where T : class
    {
        var value = await cache.GetAsync(key, cancellationToken);

        if ((value?.Length ?? 0) == 0) return null;

        return JsonSerializer.Deserialize<T>(value);
    }

    public static async Task SetAsync<T>(this IDistributedCache cache,
        string key,
        T value,
        DistributedCacheEntryOptions? cacheOptions = null,
        CancellationToken cancellationToken = default)
    {
        await cache.SetAsync(key,
            JsonSerializer.SerializeToUtf8Bytes(value),
            cacheOptions ?? DefaultCacheOptions,
            cancellationToken);
    }

    public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache,
        string key,
        Func<Task<T?>> setCache,
        DistributedCacheEntryOptions? cacheOptions = null,
        CancellationToken cancellationToken = default) where T : class
    {
        var cachedValues = await cache.GetAsync<T>(key, cancellationToken);

        if (cachedValues != null) return cachedValues;

        if (setCache == null) return null;

        var incomingValues = await setCache();

        if (incomingValues == null) return null;

        await cache.SetAsync(key, incomingValues, cacheOptions, cancellationToken);

        return incomingValues;
    }
}