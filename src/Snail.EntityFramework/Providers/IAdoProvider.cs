using System.Data;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库访问提供程序
/// </summary>
public interface IAdoProvider
{
    #region SqlQuerySingle

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
    T SqlQuerySingle<T>(string sql, params SqlParameter[] parameters);

    #endregion

    #region SqlQuery

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
    List<T> SqlQuery<T>(string sql, params SqlParameter[] parameters);

    #endregion

    #region ExecuteCommand

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
    int ExecuteCommand(string sql, params SqlParameter[] parameters);

    #endregion

    #region GetDataReader

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
    IDataReader GetDataReader(string sql, params SqlParameter[] parameters);

    #endregion

    #region GetDataSet

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
    DataSet GetDataSet(string sql, params SqlParameter[] parameters);

    #endregion

    #region GetDataTable

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
    DataTable GetDataTable(string sql, params SqlParameter[] parameters);

    #endregion
}