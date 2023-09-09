using System.Data;
using Snail.EntityFramework.Exceptions;

namespace Snail.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据参数化类型转换提供器
/// </summary>
public class SqlParameterTypeConvertProvider : ISqlParameterTypeConvertProvider
{
    /// <summary>
    ///     类型对象转换数据类型
    /// </summary>
    /// <param name="type">类型对象</param>
    public DbType ConvertDataType(Type type)
    {
        return type.Name switch
        {
            "Int32" => DbType.Int32,
            "DateTime" => DbType.DateTime,
            "DateTimeOffset" => DbType.DateTimeOffset,
            _ => throw new EntityFrameworkException($"数据参数化类型转换失败，原因：参数类型{type.Name}无法适配数据类型")
        };
    }
}