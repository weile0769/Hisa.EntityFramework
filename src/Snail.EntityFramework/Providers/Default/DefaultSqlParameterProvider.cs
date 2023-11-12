using System.Collections;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据参数化提供器
/// </summary>
public class DefaultSqlParameterProvider : ISqlParameterProvider
{
    /// <summary>
    ///     数据参数化类型转换提供器
    /// </summary>
    private readonly ISqlParameterTypeConvertProvider _typeConvert;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultSqlParameterProvider(ISqlParameterTypeConvertProvider typeConvert)
    {
        _typeConvert = typeConvert;
    }

    /// <summary>
    ///     获取数据参数
    /// </summary>
    /// <param name="objectParameter">对象参数</param>
    /// <returns>数据参数数组</returns>
    public SqlParameter[] GetSqlParameterByObject(object objectParameter)
    {
        var sqlParameters = new List<SqlParameter>();
        if (objectParameter != null)
        {
            switch (objectParameter)
            {
                case SqlParameter sqlParameter:
                    sqlParameters.Add(sqlParameter);
                    break;
                case SqlParameter[] parameters:
                    sqlParameters.AddRange(parameters);
                    break;
                default:
                {
                    var entityType = objectParameter.GetType();
                    var properties = entityType.GetProperties();
                    foreach (var property in properties)
                    {
                        var parameter = new SqlParameter();
                        var propertyValue = property.GetValue(objectParameter, null);
                        if (propertyValue == null || propertyValue.Equals(DateTime.MinValue))
                        {
                            propertyValue = DBNull.Value;
                        }
                        else
                        {
                            if (propertyValue.GetType().IsEnum)
                            {
                                propertyValue = Convert.ToInt64(propertyValue);
                            }

                            if (propertyValue is not (Array or IList))
                            {
                                parameter.DbType = _typeConvert.ConvertDataType(propertyValue.GetType());
                            }
                        }

                        parameter.ParameterName = property.Name;
                        parameter.Value = propertyValue;
                        sqlParameters.Add(parameter);
                    }

                    break;
                }
            }
        }

        return sqlParameters.ToArray();
    }
}