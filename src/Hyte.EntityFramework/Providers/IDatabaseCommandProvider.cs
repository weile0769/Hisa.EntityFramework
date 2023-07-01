using System.Data;
using Hyte.EntityFramework.Models;

namespace Hyte.EntityFramework;

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
    /// <returns>数据库命令</returns>
    IDbCommand GetCommand(string sql, SqlParameter[] parameters);
}