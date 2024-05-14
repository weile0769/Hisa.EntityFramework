using System.Data;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     数据参数化类型转换提供器
/// </summary>
public interface ISqlParameterTypeConvertProvider
{
    /// <summary>
    ///     类型对象转换数据类型
    /// </summary>
    /// <param name="type">类型对象</param>
    DbType ConvertDataType(Type type);
}