using Microsoft.Extensions.Caching.Memory;

namespace Snail.EntityFramework;

/// <summary>
///     缓存服务提供器（本地内存默认实现）
/// </summary>
public class MemoryCacheProvider : ICacheProvider
{
    /// <summary>
    ///     内存缓存服务
    /// </summary>
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    ///     构造函数
    /// </summary>
    public MemoryCacheProvider(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    ///     获取缓存数据
    /// </summary>
    /// <remarks>
    ///     不存在，则创建后再返回最新缓存数据
    /// </remarks>
    /// <param name="cacheKey">缓存键</param>
    /// <param name="create">创建数据委托函数</param>
    /// <param name="cacheTimeSpan">过期时间，可空</param>
    /// <returns>缓存数据</returns>
    public T GetOrAdd<T>(string cacheKey, Func<T> create, TimeSpan? cacheTimeSpan = default)
    {
        if (_memoryCache.TryGetValue(cacheKey, out T cacheValue))
        {
            cacheValue = create();
            if (cacheTimeSpan.HasValue)
            {
                _memoryCache.Set(cacheKey, cacheValue, cacheTimeSpan.Value);
            }
            else
            {
                _memoryCache.Set(cacheKey, cacheValue);
            }
        }

        return cacheValue;
    }
}