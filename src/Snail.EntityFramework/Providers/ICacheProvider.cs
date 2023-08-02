namespace Snail.EntityFramework;

/// <summary>
///     缓存服务提供器
/// </summary>
public interface ICacheProvider
{
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
    T GetOrAdd<T>(string cacheKey, Func<T> create, TimeSpan? cacheTimeSpan = default);
}