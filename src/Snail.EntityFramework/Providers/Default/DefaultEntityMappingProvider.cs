using System.Reflection;
using Snail.EntityFramework.Caching;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     实体映射提供器
/// </summary>
public class DefaultEntityMappingProvider : IEntityMappingProvider
{
    /// <summary>
    ///     缓存服务提供器
    /// </summary>
    private readonly ICacheProvider _cache;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultEntityMappingProvider(ICacheProvider cache)
    {
        _cache = cache;
    }

    /// <summary>
    ///     获取实体信息（无缓存）
    /// </summary>
    /// <param name="type">实体类型</param>
    /// <returns>实体信息</returns>
    public Entity GetEntityNoCache(Type type)
    {
        var entity = new Entity();
        var sqlTableAttribute = type.GetTypeInfo().GetCustomAttributes(typeof(SqlTable), false).SingleOrDefault(it => it is SqlTable);
        if (sqlTableAttribute != null)
        {
            var table = (SqlTable)sqlTableAttribute;
            entity.TableName = table.TableName;
            entity.TableDescription = table.TableDescription;
        }
        else
        {
            entity.TableName = type.Name;
        }

        entity.Type = type;
        entity.Name = type.Name;
        entity.InitEntityColumns();
        return entity;
    }

    /// <summary>
    ///     获取实体信息（缓存）
    /// </summary>
    /// <param name="type">实体类型</param>
    /// <returns>实体信息</returns>
    public Entity GetEntity(Type type)
    {
        var cacheKey = $"EntityMappingTable:{type.FullName}";
        return _cache.GetOrAdd(cacheKey, () => GetEntityNoCache(type));
    }
}