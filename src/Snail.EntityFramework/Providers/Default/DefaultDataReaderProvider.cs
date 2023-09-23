using System.Data;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

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
    public IDataReader GetDataReader(string sql, params SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }
}