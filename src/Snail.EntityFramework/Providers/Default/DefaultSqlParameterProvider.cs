using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据参数化提供器
/// </summary>
public class DefaultSqlParameterProvider : ISqlParameterProvider
{
    /// <summary>
    ///     获取数据参数
    /// </summary>
    /// <param name="objectParameter">对象参数</param>
    /// <returns>数据参数数组</returns>
    public SqlParameter[] GetSqlParameter(object objectParameter)
    {
        var sqlParameters = new List<SqlParameter>();
        if (objectParameter != null)
        {
            var entityType = objectParameter.GetType();
            var properties = entityType.GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(objectParameter, null);
                if (propertyValue == null || propertyValue.Equals(DateTime.MinValue))
                {
                    propertyValue = DBNull.Value;
                }

                var parameter = new SqlParameter(property.Name, propertyValue);
                sqlParameters.Add(parameter);
            }
        }

        return sqlParameters.ToArray();
    }
}