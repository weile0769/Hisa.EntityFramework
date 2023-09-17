using System.Data.Common;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库访问提供程序默认实现
/// </summary>
public class DefaultAdoProvider : IAdoProvider
{
    /// <summary>
    ///     数据库读取提供器
    /// </summary>
    private readonly IDataReaderProvider _dataReader;

    /// <summary>
    ///     数据库读取转换泛型实体提供器
    /// </summary>
    private readonly IDataReaderTypeConvertProvider _dataReaderTypeConvert;

    /// <summary>
    ///     数据参数化提供器
    /// </summary>
    private readonly ISqlParameterProvider _parameterReader;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultAdoProvider(IDataReaderProvider dataReader, 
        ISqlParameterProvider parameterReader,
        IDataReaderTypeConvertProvider dataReaderTypeConvert
    )
    {
        _dataReader = dataReader;
        _parameterReader = parameterReader;
        _dataReaderTypeConvert = dataReaderTypeConvert;
    }

    #region SqlQuerySingle

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    public T SqlQuerySingle<T>(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return SqlQuerySingle<T>(sql, parameters);
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    public T SqlQuerySingle<T>(string sql, params SqlParameter[] parameters)
    {
        using var dataReader = _dataReader.GetDataReader(sql, parameters);
        var entity = default(T);
        if (((DbDataReader)dataReader).HasRows)
        {
            entity = _dataReaderTypeConvert.ToEntity<T>(dataReader);
        }

        return entity;
    }

    #endregion

    #region SqlQuery

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
        using var dataReader = _dataReader.GetDataReader(sql, parameters);
        var entities = new List<T>();
        if (((DbDataReader)dataReader).HasRows)
        {
            entities = _dataReaderTypeConvert.ToEntities<T>(dataReader);
        }

        return entities;
    }

    #endregion
}