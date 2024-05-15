namespace Hisa.EntityFramework.Providers;

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


    /// <summary>
    ///     获取缓存值
    /// </summary>
    /// <param name="cacheKey">缓存键</param>
    /// <typeparam name="T">缓存对象类</typeparam>
    /// <returns>缓存对象</returns>
    T Get<T>(string cacheKey);

    /// <summary>
    ///     设置缓存
    /// </summary>
    /// <param name="cacheKey">缓存键</param>
    /// <param name="item">缓存对象</param>
    /// <param name="duration">过期时间</param>
    /// <typeparam name="T">缓存对象类</typeparam>
    /// <returns>任务状态</returns>
    void Set<T>(string cacheKey, T item, TimeSpan duration);
}