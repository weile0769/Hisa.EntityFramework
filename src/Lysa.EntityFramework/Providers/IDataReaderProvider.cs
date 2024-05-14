using System.Data.Common;
using Lysa.EntityFramework.Models;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     数据库读取提供器
/// </summary>
public interface IDataReaderProvider
{
    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    DbDataReader GetDataReader(string sql, params SqlParameter[] parameters);

    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns></returns>
    Task<DbDataReader> GetDataReaderAsync(string sql, CancellationToken token = default);

    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns></returns>
    Task<DbDataReader> GetDataReaderAsync(string sql, SqlParameter[] parameters, CancellationToken token = default);
}