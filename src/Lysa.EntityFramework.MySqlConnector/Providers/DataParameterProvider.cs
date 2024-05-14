using System.Data;
using MySqlConnector;
using Lysa.EntityFramework.Models;
using Lysa.EntityFramework.MySqlConnector.Utils;
using Lysa.EntityFramework.Providers;

namespace Lysa.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库参数提供器
/// </summary>
public class DataParameterProvider : IDataParameterProvider
{
    /// <summary>
    ///     获取数据库参数
    /// </summary>
    /// <param name="parameters">自定义参数</param>
    /// <returns>数据库参数</returns>
    public IDataParameter[] GetDataParameter(SqlParameter[] parameters)
    {
        if (parameters == null || parameters.Length == 0)
        {
            return null;
        }

        var mySqlParameters = new MySqlParameter[parameters.Length];
        var index = 0;
        foreach (var parameter in parameters)
        {
            parameter.Value ??= DBNull.Value;
            var mySqlParameter = new MySqlParameter
            {
                ParameterName = parameter.ParameterName,
                Size = parameter.Size,
                Value = parameter.Value,
                DbType = parameter.DbType
            };
            if (parameter.Direction == 0)
            {
                parameter.Direction = ParameterDirection.Input;
            }

            mySqlParameter.Direction = parameter.Direction;
            if (parameter.DbType == DbType.DateTimeOffset)
            {
                if (mySqlParameter.Value != DBNull.Value)
                {
                    mySqlParameter.Value = UtilMethods.ConvertFromDateTimeOffset((DateTimeOffset)mySqlParameter.Value);
                }

                mySqlParameter.DbType = DbType.DateTime;
            }

            mySqlParameters[index] = mySqlParameter;
            ++index;
        }

        return mySqlParameters;
    }
}