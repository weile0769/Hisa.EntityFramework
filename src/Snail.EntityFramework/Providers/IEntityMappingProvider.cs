using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     实体映射提供器
/// </summary>
public interface IEntityMappingProvider
{
    /// <summary>
    ///     获取实体信息
    /// </summary>
    /// <param name="type">实体类型</param>
    /// <returns>实体信息</returns>
    Entity GetEntity(Type type);
}