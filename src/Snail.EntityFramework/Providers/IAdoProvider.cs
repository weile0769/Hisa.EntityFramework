using System.Data;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework;

/// <summary>
///     数据库访问提供程序
/// </summary>
public interface IAdoProvider
{
    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="connection">数据库连接对象</param>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    IDataReader GetDataReader(IDbConnection connection, string sql, params SqlParameter[] parameters);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    List<T> SqlQuery<T>(string sql, params SqlParameter[] parameters);
}