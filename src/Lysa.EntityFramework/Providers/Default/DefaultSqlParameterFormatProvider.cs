using System.Collections;
using System.Text.RegularExpressions;
using Lysa.EntityFramework.Models;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     特殊查询参数过滤器
/// </summary>
public class DefaultSqlParameterFormatProvider : ISqlParameterFormatProvider
{
    /// <summary>
    /// </summary>
    private readonly ISqlParameterFormatValueProvider _formatValueProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultSqlParameterFormatProvider(ISqlParameterFormatValueProvider sqlSlashesProvider)
    {
        _formatValueProvider = sqlSlashesProvider;
    }

    /// <summary>
    ///     过滤特殊命令（SQL）
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询结果对象类型</param>
    /// <returns>SQL</returns>
    public string FormatSql(string sql, SqlParameter[] parameters)
    {
        if (!parameters.HasValue())
        {
            return sql;
        }

        foreach (var parameter in parameters)
        {
            if (parameter == null)
            {
                continue;
            }

            switch (parameter.Value)
            {
                case null:
                    parameter.Value = DBNull.Value;
                    break;
                case Array or IList:
                    sql = Regex.Replace(sql, @$"\s+(in|In|IN|iN)\s+[\(]?[\@\:\?]{parameter.ParameterName.TrimStart('@', '?', ':')}[\)]?", m =>
                    {
                        if (parameter.Value is not IEnumerable arr)
                        {
                            return " IS NULL";
                        }

                        var values = arr.Cast<object>().ToList();

                        return $" in {_formatValueProvider.FormatSqlValue(values)}";
                    });
                    break;
            }
        }

        return sql;
    }
}