using System.Data;
using System.Data.Common;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库访问提供程序
/// </summary>
public interface IAdoProvider
{
    #region 同步

    #region SqlQuerySingle

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    T SqlQuerySingle<T>(string sql);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    T SqlQuerySingle<T>(string sql, object parameter);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    T SqlQuerySingle<T>(string sql, List<SqlParameter> parameters);

    #endregion

    #region SqlQuery

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    List<T> SqlQuery<T>(string sql);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    List<T> SqlQuery<T>(string sql, object parameter);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    List<T> SqlQuery<T>(string sql, List<SqlParameter> parameters);

    #endregion

    #region ExecuteCommand

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>影响行数</returns>
    int ExecuteCommand(string sql);

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>影响行数</returns>
    int ExecuteCommand(string sql, object parameter);

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>影响行数</returns>
    int ExecuteCommand(string sql, List<SqlParameter> parameters);

    #endregion

    #region GetScalar

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>首行首列</returns>
    object GetScalar(string sql);

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>首行首列</returns>
    object GetScalar(string sql, object parameter);

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>首行首列</returns>
    object GetScalar(string sql, List<SqlParameter> parameters);

    #endregion

    #region GetDataReader

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>数据读取器</returns>
    IDataReader GetDataReader(string sql);

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>数据读取器</returns>
    IDataReader GetDataReader(string sql, object parameter);

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据读取器</returns>
    IDataReader GetDataReader(string sql, List<SqlParameter> parameters);

    #endregion

    #region GetDataSet

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>数据结果集</returns>
    DataSet GetDataSet(string sql);

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>数据结果集</returns>
    DataSet GetDataSet(string sql, object parameter);

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据结果集</returns>
    DataSet GetDataSet(string sql, List<SqlParameter> parameters);

    #endregion

    #region GetDataTable

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>数据表格</returns>
    DataTable GetDataTable(string sql);

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>数据表格</returns>
    DataTable GetDataTable(string sql, object parameter);

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据表格</returns>
    DataTable GetDataTable(string sql, List<SqlParameter> parameters);

    #endregion

    #endregion

    #region 异步

    #region SqlQuerySingleAsync

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    Task<T> SqlQuerySingleAsync<T>(string sql, CancellationToken token = default);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    Task<T> SqlQuerySingleAsync<T>(string sql, object parameter, CancellationToken token = default);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    Task<T> SqlQuerySingleAsync<T>(string sql, List<SqlParameter> parameters, CancellationToken token = default);

    #endregion

    #region SqlQueryAsync

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    Task<List<T>> SqlQueryAsync<T>(string sql, CancellationToken token = default);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    Task<List<T>> SqlQueryAsync<T>(string sql, object parameter, CancellationToken token = default);

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    Task<List<T>> SqlQueryAsync<T>(string sql, List<SqlParameter> parameters, CancellationToken token = default);

    #endregion

    #region ExecuteCommandAsync

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    Task<int> ExecuteCommandAsync(string sql, CancellationToken token = default);

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    Task<int> ExecuteCommandAsync(string sql, object parameter, CancellationToken token = default);

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    Task<int> ExecuteCommandAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default);

    #endregion

    #region GetScalarAsync

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>首行首列</returns>
    Task<object> GetScalarAsync(string sql);

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>首行首列</returns>
    Task<object> GetScalarAsync(string sql, object parameter);

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>首行首列</returns>
    Task<object> GetScalarAsync(string sql, List<SqlParameter> parameters);

    #endregion

    #region GetDataReaderAsync

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    Task<DbDataReader> GetDataReaderAsync(string sql, CancellationToken token = default);

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    Task<DbDataReader> GetDataReaderAsync(string sql, object parameter, CancellationToken token = default);

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    Task<DbDataReader> GetDataReaderAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default);

    #endregion

    #region GetDataSetAsync

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    Task<DataSet> GetDataSetAsync(string sql, CancellationToken token = default);


    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    Task<DataSet> GetDataSetAsync(string sql, object parameter, CancellationToken token = default);


    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    Task<DataSet> GetDataSetAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default);

    #endregion

    #region GetDataTableAsync

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据表格</returns>
    Task<DataTable> GetDataTableAsync(string sql, CancellationToken token = default);

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据表格</returns>
    Task<DataTable> GetDataTableAsync(string sql, object parameter, CancellationToken token = default);


    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据表格</returns>
    Task<DataTable> GetDataTableAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default);

    #endregion

    #endregion
}