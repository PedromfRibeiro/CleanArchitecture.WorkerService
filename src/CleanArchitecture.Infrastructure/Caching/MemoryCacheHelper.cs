using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.Infrastructure.Caching;

public class MemoryCacheHelper
{
    private readonly IMemoryCache _cache;

    public MemoryCacheHelper()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    /// <summary>
    /// Clears the entire cache.
    /// </summary>
    public void Clear() => (_cache as MemoryCache)?.Compact(1.0);

    /// <summary>
    /// Deletes an item from the cache.
    /// </summary>
    public bool Delete(string key)
    {
        if (_cache.TryGetValue(key, out _))
        {
            _cache.Remove(key);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Retrieves an item from the cache.
    /// </summary>
    public T? Get<T>(string key) => _cache.TryGetValue(key, out T? value) ? value : default;

    /// <summary>
    /// Adds or updates an item in the cache.
    /// </summary>
    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
        };
        _cache.Set(key, value, cacheEntryOptions);
    }

    /// <summary>
    /// Updates an existing item in the cache.
    /// </summary>
    public bool Update<T>(string key, T newValue, TimeSpan? expiration = null)
    {
        if (_cache.TryGetValue(key, out _))
        {
            Set(key, newValue, expiration);
            return true;
        }
        return false;
    }
}
