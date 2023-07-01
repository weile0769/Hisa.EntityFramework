using System.Data;
using Hyte.EntityFramework.Models;
using Hyte.EntityFramework.Options;
using MySqlConnector;

namespace Hyte.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库命令提供器
/// </summary>
public class DatabaseCommandProvider : IDatabaseCommandProvider
{
    /// <summary>
    ///     数据库参数提供器
    /// </summary>
    private readonly IDataParameterProvider _dataParameterProvider;

    /// <summary>
    ///     数据库连接对象提供器
    /// </summary>
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider;

    /// <summary>
    ///     数据库连接配置选项
    /// </summary>
    private readonly MySqlConnectorOptions _options;

    /// <summary>
    ///     数据库命令类型
    /// </summary>
    private CommandType _commandType;

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
    public DatabaseCommandProvider(MySqlConnectorOptions options,
        IDatabaseConnectionProvider databaseConnectionProvider,
        IDataParameterProvider dataParameterProvider)
    {
        _options = options;
        _commandType = CommandType.Text;
        _databaseConnectionProvider = databaseConnectionProvider;
        _dataParameterProvider = dataParameterProvider;
    }

    /// <summary>
    ///     获取数据库命令
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>数据库命令</returns>
    public IDbCommand GetCommand(string sql, SqlParameter[] parameters)
    {
        if (_dbCommand == null)
        {
            var connection = _databaseConnectionProvider.GetConnection();
            var sqlCommand = new MySqlCommand(sql, (MySqlConnection)connection);
            sqlCommand.CommandType = _commandType;
            sqlCommand.CommandTimeout = _options.CommandTimeOut;
            if (_transaction != null)
            {
                sqlCommand.Transaction = (MySqlTransaction)_transaction;
            }

            if (parameters.HasValue())
            {
                var dataParameters = _dataParameterProvider.GetDataParameter(parameters);
                sqlCommand.Parameters.AddRange((MySqlParameter[])dataParameters);
            }

            _dbCommand = sqlCommand;
        }

        _databaseConnectionProvider.CheckConnection();
        return _dbCommand;
    }

    /// <summary>
    ///     自动销毁
    /// </summary>
    public void Dispose()
    {
        _dbCommand?.Dispose();
    }

    /// <summary>
    ///     设置数据库命令类型
    /// </summary>
    /// <param name="commandType">数据库命令类型</param>
    public void SetCommandType(CommandType commandType)
    {
        _commandType = commandType;
    }

    /// <summary>
    ///     设置数据库事务对象
    /// </summary>
    /// <param name="transaction">数据库事务对象</param>
    public void SetDbTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }
}