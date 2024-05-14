using System.Reflection;
using Lysa.EntityFramework.Models;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     实体映射提供器
/// </summary>
public class DefaultEntityMappingProvider : IEntityMappingProvider
{
    /// <summary>
    ///     获取实体信息
    /// </summary>
    /// <param name="type">实体类型</param>
    /// <returns>实体信息</returns>
    public Entity GetEntity(Type type)
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
}