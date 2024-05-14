using Lysa.EntityFramework.Models;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     数据参数化提供器
/// </summary>
public interface ISqlParameterProvider
{
    /// <summary>
    ///     获取数据参数
    /// </summary>
    /// <param name="objectParameter">对象参数</param>
    /// <returns>数据参数数组</returns>
    SqlParameter[] GetSqlParameterByObject(object objectParameter);
}