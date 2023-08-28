using System.Data;
using MySqlConnector;
using Snail.EntityFramework.Models;
using Snail.EntityFramework.Options;

namespace Snail.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库命令提供器
/// </summary>
public class DatabaseCommandProvider : IDatabaseCommandProvider
{
    /// <summary>
    ///     数据库连接配置选项提供器
    /// </summary>
    private readonly IDatabaseConnectorOptionsProvider _connectorOptions;

    /// <summary>
    ///     数据库参数提供器
    /// </summary>
    private readonly IDataParameterProvider _parameter;

    /// <summary>
    ///     数据库命令类型
    /// </summary>
    private readonly CommandType _commandType;

    /// <summary>
    ///     数据库命令
    /// </summary>
    private IDbCommand _dbCommand;

    /// <summary>
    ///     数据库事务对象
    /// </summary>
    private IDbTransaction _transaction;


    /// <summary>
    ///     构造函数
    /// </summary>
    public DatabaseCommandProvider(IDataParameterProvider parameter,
        IDatabaseConnectorOptionsProvider connectorOptions)
    {
        _commandType = CommandType.Text;
        _parameter = parameter;
        _connectorOptions = connectorOptions;
    }

    /// <summary>
    ///     自动销毁
    /// </summary>
    public void Dispose()
    {
        _dbCommand?.Dispose();
    }

    /// <summary>
    ///     获取数据库命令
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="parameters">sql参数</param>
    /// <param name="connection">数据库连接对象</param>
    /// <returns>数据库命令</returns>
    public IDbCommand GetCommand(string sql, SqlParameter[] parameters, IDbConnection connection)
    {
        if (_dbCommand == null)
        {
            var connectorOptions = _connectorOptions.GetCurrentConnectorOptions();
            var sqlCommand = new MySqlCommand(sql, (MySqlConnection)connection);
            sqlCommand.CommandType = _commandType;
            sqlCommand.CommandTimeout = connectorOptions.CommandTimeOut;
            if (_transaction != null)
            {
                sqlCommand.Transaction = (MySqlTransaction)_transaction;
            }

            if (parameters.HasValue())
            {
                var dataParameters = _parameter.GetDataParameter(parameters);
                sqlCommand.Parameters.AddRange((MySqlParameter[])dataParameters);
            }

            _dbCommand = sqlCommand;
        }

        return _dbCommand;
    }
}