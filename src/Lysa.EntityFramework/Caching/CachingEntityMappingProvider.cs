using Microsoft.Extensions.Logging;
using Lysa.EntityFramework.Models;
using Lysa.EntityFramework.Providers;

namespace Lysa.EntityFramework.Caching;

/// <summary>
///     实体映射提供器缓存
/// </summary>
public class CachingEntityMappingProvider<T> : IEntityMappingProvider
    where T : IEntityMappingProvider
{
    /// <summary>
    ///     缓存键
    /// </summary>
    private const string CacheKey = "lysa:reader:entity:mapping:{0}";

    /// <summary>
    ///     缓存服务
    /// </summary>
    private readonly ICacheProvider _cacheProvider;

    /// <summary>
    ///     实体映射提供器
    /// </summary>
    private readonly IEntityMappingProvider _entityMappingProvider;

    /// <summary>
    ///     日志服务
    /// </summary>
    private readonly ILogger<CachingEntityMappingProvider<T>> _logger;

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <returns></returns>
    public CachingEntityMappingProvider(ICacheProvider cacheProvider,
        T entityMappingProvider,
        ILogger<CachingEntityMappingProvider<T>> logger)
    {
        _logger = logger;
        _cacheProvider = cacheProvider;
        _entityMappingProvider = entityMappingProvider;
    }

    /// <summary>
    ///     获取实体信息（缓存）
    /// </summary>
    /// <param name="type">实体类型</param>
    /// <returns>实体信息</returns>
    public Entity GetEntity(Type type)
    {
        var fullName = type.FullName;
        var builder = _cacheProvider.Get(string.Format(CacheKey, $"EntityMapping.{fullName}"),
            TimeSpan.FromDays(366),
            () => _entityMappingProvider.GetEntity(type),
            _logger);

        return builder;
    }
}