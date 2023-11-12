using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     特殊查询参数过滤器
/// </summary>
public interface ISqlParameterFormatProvider
{
    /// <summary>
    ///     过滤特殊命令（SQL）
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询结果对象类型</param>
    /// <returns>SQL</returns>
    string FormatSql(string sql, SqlParameter[] parameters);
}