using Microsoft.Extensions.Caching.Distributed;
using Order.Application.Interfaces;
using System.Text.Json;

namespace Order.Infrastructure.Cache;

internal class CacheService(IDistributedCache cache) : ICacheService
{
    public T GetCachedData<T>(string key)
    {
        var jsonData = cache.GetString(key);
        if (jsonData == null)
            return default(T);

        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public void SetCachedData<T>(string key, T data, TimeSpan cacheDuration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheDuration
        };
        var jsonData = JsonSerializer.Serialize(data);
        cache.SetString(key, jsonData, options);
    }
    public void DeleteCachedData(string key)
    {
        cache.Remove(key);
    }
}