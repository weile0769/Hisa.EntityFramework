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
}