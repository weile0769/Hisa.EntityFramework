using System.Data;
using Snail.EntityFramework.Exceptions;
using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据参数化类型转换提供器默认实现
/// </summary>
public class DefaultSqlParameterTypeConvertProvider : ISqlParameterTypeConvertProvider
{
    /// <summary>
    ///     类型对象转换数据类型
    /// </summary>
    /// <param name="type">类型对象</param>
    public DbType ConvertDataType(Type type)
    {
        if (type is { IsEnum: true })
        {
            return DbType.Int64;
        }

        return type.Name switch
        {
            "UInt16" => DbType.UInt16,
            "Int16" => DbType.Int16,
            "UInt32" => DbType.UInt32,
            "Int32" => DbType.Int32,
            "UInt64" => DbType.UInt64,
            "Int64" => DbType.Int64,
            "Byte" => DbType.Byte,
            "SByte" => DbType.SByte,
            "Boolean" => DbType.Boolean,
            "Single" => DbType.Single,
            "Double" => DbType.Double,
            "Decimal" => DbType.Decimal,
            "String" => DbType.String,
            "Guid" => DbType.Guid,
            "DateTime" => DbType.DateTime,
            "DateTimeOffset" => DbType.DateTimeOffset,
            "TimeSpan" => DbType.Time,
            "Byte[]" => DbType.Binary,
            _ => throw new EntityFrameworkException($"数据参数化类型转换失败，原因：参数类型{type.Name}无法适配数据类型")
        };
    }
}