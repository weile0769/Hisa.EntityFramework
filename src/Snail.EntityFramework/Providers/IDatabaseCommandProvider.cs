using System.Data;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework;

/// <summary>
///     数据库命令提供器
/// </summary>
public interface IDatabaseCommandProvider:IDisposable
{
    /// <summary>
    ///     获取数据库命令
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="parameters">sql参数</param>
    /// <param name="connection">数据库连接对象</param>
    /// <returns>数据库命令</returns>
    IDbCommand GetCommand(string sql, SqlParameter[] parameters, IDbConnection connection);
}