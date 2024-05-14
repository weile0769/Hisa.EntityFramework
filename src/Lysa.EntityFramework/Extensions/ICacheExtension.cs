using Microsoft.Extensions.Logging;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     ICache扩展类
/// </summary>
internal static class ICacheExtension
{
    /// <summary>
    ///     获取缓存值
    /// </summary>
    /// <param name="cacheProvider">ICache接口</param>
    /// <param name="cacheKey">缓存键</param>
    /// <param name="duration">过期时间</param>
    /// <param name="get">获取数据工厂</param>
    /// <param name="logger">日志服务</param>
    /// <typeparam name="T">缓存对象类</typeparam>
    /// <returns>缓存对象</returns>
    public static T Get<T>(this ICacheProvider cacheProvider, string cacheKey, TimeSpan duration, Func<T> get, ILogger logger)
        where T : class
    {
        if (cacheProvider == null)
        {
            throw new ArgumentNullException(nameof(cacheProvider));
        }

        if (get == null)
        {
            throw new ArgumentNullException(nameof(get));
        }

        if (cacheKey == null)
        {
            return null;
        }

        var item = cacheProvider.Get<T>(cacheKey);

        if (item == null)
        {
            logger.LogTrace("缓存为空，缓存键：{cacheKey}", cacheKey);

            item = get();

            if (item != null)
            {
                logger.LogTrace("正在设置缓存，缓存键：{cacheKey}", cacheKey);
                cacheProvider.Set(cacheKey, item, duration);
            }
        }
        else
        {
            logger.LogTrace("正常获取缓存，缓存键：{cacheKey}", cacheKey);
        }

        return item;
    }
}