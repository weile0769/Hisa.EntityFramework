using System.Data;
using Hyte.EntityFramework.Models;
using Hyte.EntityFramework.Options;
using Hyte.EntityFramework.Utils;
using MySqlConnector;

namespace Hyte.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库访问提供程序默认实现
/// </summary>
public class AdoProvider : IAdoProvider
{
    private readonly MySqlConnectorOptions _entityFrameworkOptions;
    private IDbConnection _connection;

    public AdoProvider(MySqlConnectorOptions entityFrameworkOptions)
    {
        _entityFrameworkOptions = entityFrameworkOptions;
    }

    /// <summary>
    ///     命令执行
    /// </summary>
    public int CommandTimeOut { get; set; } = 300;

    /// <summary>
    ///     命令类型
    /// </summary>
    public CommandType CommandType { get; set; } = CommandType.Text;

    /// <summary>
    ///     事务对象
    /// </summary>
    public IDbTransaction Transaction { get; set; }

    /// <summary>
    ///     数据库连接对象
    /// </summary>
    public IDbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                try
                {
                    _connection = new MySqlConnection(_entityFrameworkOptions.ConnectionString);
                }
                catch (Exception ex)
                {
                    FrameCheck.Exception(ex.Message, ex);
                }
            }

            return _connection;
        }
    }

    /// <summary>
    ///     打开数据库连接
    /// </summary>
    public void Open()
    {
        CheckConnection();
    }

    /// <summary>
    ///     关闭数据库连接
    /// </summary>
    public void Close()
    {
        if (Connection is { State: ConnectionState.Open })
        {
            try
            {
                Connection.Close();
            }
            catch (Exception ex)
            {
                FrameCheck.Exception(ErrorMessage.ConnectionFailed, ex.Message);
            }
        }
    }

    /// <summary>
    ///     获取数据库命令对象
    /// </summary>
    /// <param name="sql">SQL脚本语句</param>
    /// <param name="parameters">SQL参数</param>
    /// <returns></returns>
    public IDbCommand GetCommand(string sql, params SqlParameter[] parameters)
    {
        var sqlCommand = new MySqlCommand(sql, (MySqlConnection)Connection);
        sqlCommand.CommandType = CommandType;
        sqlCommand.CommandTimeout = CommandTimeOut;
        if (Transaction != null)
        {
            sqlCommand.Transaction = (MySqlTransaction)Transaction;
        }

        if (parameters.HasValue())
        {
            var dataParameters = ConvertToDataParameter(parameters);
            sqlCommand.Parameters.AddRange((MySqlParameter[])dataParameters);
        }

        Open();
        return sqlCommand;
    }

    /// <summary>
    ///     SqlParameter[]转IDataParameter[]
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public IDataParameter[] ConvertToDataParameter(params SqlParameter[] parameters)
    {
        if (parameters == null || parameters.Length == 0)
        {
            return null;
        }

        var parameterArray = new IDataParameter[parameters.Length];
        var index = 0;
        foreach (var parameter in parameters)
        {
            parameter.Value ??= DBNull.Value;
            parameter.Direction = parameter.Direction == 0 ? ParameterDirection.Input : parameter.Direction;
            var sqlParameter = new MySqlParameter
            {
                ParameterName = parameter.ParameterName,
                Size = parameter.Size,
                Value = parameter.Value,
                DbType = parameter.DbType,
                Direction = parameter.Direction
            };
            switch (sqlParameter.DbType)
            {
                case DbType.String:
                    sqlParameter.DbType = DbType.AnsiString;
                    break;
                default:
                {
                    if (parameter.DbType == DbType.DateTimeOffset)
                    {
                        if (sqlParameter.Value != DBNull.Value)
                        {
                            sqlParameter.Value = ((DateTimeOffset)sqlParameter.Value).ConvertToDateTime();
                        }

                        sqlParameter.DbType = DbType.DateTime;
                    }

                    break;
                }
            }

            parameterArray[index] = sqlParameter;
            ++index;
        }

        return parameterArray;
    }

    /// <summary>
    ///     检查当前连接状态为打开状态，则将打开数据库连接
    /// </summary>
    private void CheckConnection()
    {
        if (Connection.State != ConnectionState.Open)
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                FrameCheck.Exception(ErrorMessage.ConnectionFailed, ex.Message);
            }
        }
    }
}