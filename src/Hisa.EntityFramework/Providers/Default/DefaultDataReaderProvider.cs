using System.Data;
using System.Data.Common;
using Hisa.EntityFramework.Models;

namespace Hisa.EntityFramework.Providers;

/// <summary>
///     数据库读取提供器
/// </summary>
public class DefaultDataReaderProvider : IDataReaderProvider
{
    /// <summary>
    ///     数据库命令提供器
    /// </summary>
    private readonly IDatabaseCommandProvider _command;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultDataReaderProvider(IDatabaseCommandProvider command)
    {
        _command = command;
    }

    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public DbDataReader GetDataReader(string sql, params SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }

    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns></returns>
    public Task<DbDataReader> GetDataReaderAsync(string sql, CancellationToken token = default)
    {
        var command = _command.GetCommand(sql);
        return command.ExecuteReaderAsync(CommandBehavior.CloseConnection, token);
    }

    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns></returns>
    public Task<DbDataReader> GetDataReaderAsync(string sql, SqlParameter[] parameters, CancellationToken token = default)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteReaderAsync(CommandBehavior.CloseConnection, token);
    }
}