using System.Collections;
using Snail.EntityFramework.Models;
using Snail.EntityFramework.MySqlConnector.Utils;

namespace Snail.EntityFramework.MySqlConnector.Extensions;

/// <summary>
///     Sql字符串扩展类
/// </summary>
internal static class SqlExtension
{
    internal static string FillSqlByParameter(this string sql, SqlParameter[] parameters)
    {
        return string.Empty;
    }
}