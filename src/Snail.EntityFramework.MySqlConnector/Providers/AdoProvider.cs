using System.Data;
using System.Data.Common;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库访问提供程序默认实现
/// </summary>
public class AdoProvider : IAdoProvider
{
    /// <summary>
    ///     数据库命令提供器
    /// </summary>
    private readonly IDatabaseCommandProvider _command;

    /// <summary>
    ///     数据库连接对象提供器
    /// </summary>
    private readonly IDatabaseConnectionProvider _connection;

    /// <summary>
    ///     构造函数
    /// </summary>
    public AdoProvider(IDatabaseConnectionProvider connection,
        IDatabaseCommandProvider command)
    {
        _connection = connection;
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
        using var connection = _connection.GetConnection();
        var command = _command.GetCommand(sql, parameters, connection);
        _connection.Open();
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public List<T> SqlQuery<T>(string sql, params SqlParameter[] parameters)
    {
        using (var dataReader = GetDataReader(sql, parameters))
        {
            var result = new List<T>();
            if (((DbDataReader)dataReader).HasRows)
            {
                
            }
        }
    }
}