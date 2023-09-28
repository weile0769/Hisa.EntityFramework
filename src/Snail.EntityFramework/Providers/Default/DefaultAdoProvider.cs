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
    ///     数据读取适配器
    /// </summary>
    private readonly IDataAdapterProvider _dataAdapter;

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
    public DefaultAdoProvider(IDataAdapterProvider dataAdapter,
        IDataReaderProvider dataReader,
        ISqlParameterProvider parameterReader,
        IDatabaseCommandProvider command,
        IDataReaderTypeConvertProvider dataReaderTypeConvert
    )
    {
        _dataReader = dataReader;
        _command = command;
        _dataAdapter = dataAdapter;
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

    #region ExecuteCommand

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>影响行数</returns>
    public int ExecuteCommand(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        var command = _command.GetCommand(sql, parameters);
        var count = command.ExecuteNonQuery();
        return count;
    }

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>影响行数</returns>
    public int ExecuteCommand(string sql, params SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters);
        var count = command.ExecuteNonQuery();
        return count;
    }

    #endregion

    #region GetDataReader

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>数据读取器</returns>
    public IDataReader GetDataReader(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return _dataReader.GetDataReader(sql, parameters);
    }

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据读取器</returns>
    public IDataReader GetDataReader(string sql, params SqlParameter[] parameters)
    {
        return _dataReader.GetDataReader(sql, parameters);
    }

    #endregion

    #region GetDataSet

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>数据结果集</returns>
    public DataSet GetDataSet(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return GetDataSet(sql, parameters);
    }

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据结果集</returns>
    public DataSet GetDataSet(string sql, params SqlParameter[] parameters)
    {
        var dataAdapter = _dataAdapter.GetDataAdapter();
        var command = _command.GetCommand(sql, parameters);
        _dataAdapter.SetCommandToAdapter(dataAdapter, command);
        var dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        return dataSet;
    }

    #endregion
}