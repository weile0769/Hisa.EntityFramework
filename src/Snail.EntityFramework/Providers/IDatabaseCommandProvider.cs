using System.Data.Common;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库命令提供器
/// </summary>
public interface IDatabaseCommandProvider : IDisposable, IAsyncDisposable
{
    /// <summary>
    ///     获取数据库命令
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>数据库命令</returns>
    DbCommand GetCommand(string sql, params SqlParameter[] parameters);
}