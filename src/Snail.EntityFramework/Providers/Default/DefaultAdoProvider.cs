using System.Data;
using System.Data.Common;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库访问提供程序默认实现
/// </summary>
public class DefaultAdoProvider : IAdoProvider
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
    ///     数据库读取提供器
    /// </summary>
    private readonly IDataReaderProvider _dataReader;

    /// <summary>
    ///     数据参数化提供器
    /// </summary>
    private readonly ISqlParameterProvider _parameterReader;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultAdoProvider(IDataReaderProvider dataReader,
        IDatabaseConnectionProvider connection,
        IDatabaseCommandProvider command,
        ISqlParameterProvider parameterReader)
    {
        _command = command;
        _dataReader = dataReader;
        _connection = connection;
        _parameterReader = parameterReader;
    }


    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    public List<T> SqlQuery<T>(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return SqlQuery<T>(sql, parameters);
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    public List<T> SqlQuery<T>(string sql, params SqlParameter[] parameters)
    {
        var connection = _connection.GetConnection();
        using var dataReader = GetDataReader(connection, sql, parameters);
        var entities = new List<T>();
        if (((DbDataReader)dataReader).HasRows)
        {
            entities = _dataReader.ToEntities<T>(dataReader);
        }

        return entities;
    }

    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="connection">数据库连接对象</param>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public IDataReader GetDataReader(IDbConnection connection, string sql, params SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters, connection);
        _connection.Open();
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }
}