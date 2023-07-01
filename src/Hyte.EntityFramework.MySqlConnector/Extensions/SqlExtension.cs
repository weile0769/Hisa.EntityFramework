using System.Collections;
using Hyte.EntityFramework.Models;
using Hyte.EntityFramework.MySqlConnector.Utils;

namespace Hyte.EntityFramework.MySqlConnector.Extensions;

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