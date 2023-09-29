using System.Data;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
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
    ///     容器服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultAdoProvider(IDataAdapterProvider dataAdapter,
        IDataReaderProvider dataReader,
        IServiceProvider serviceProvider,
        ISqlParameterProvider parameterReader,
        IDatabaseCommandProvider command,
        IDataReaderTypeConvertProvider dataReaderTypeConvert
    )
    {
        _dataReader = dataReader;
        _command = command;
        _serviceProvider = serviceProvider;
        _dataAdapter = dataAdapter;
        _parameterReader = parameterReader;
        _dataReaderTypeConvert = dataReaderTypeConvert;
    }

    #region 同步

    #region SqlQuerySingle

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    public T SqlQuerySingle<T>(string sql)
    {
        using var dataReader = _dataReader.GetDataReader(sql);
        var entity = default(T);
        if (dataReader.HasRows)
        {
            entity = _dataReaderTypeConvert.ToEntity<T>(dataReader);
        }

        return entity;
    }

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
    public T SqlQuerySingle<T>(string sql, List<SqlParameter> parameters)
    {
        return SqlQuerySingle<T>(sql, parameters.ToArray());
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    private T SqlQuerySingle<T>(string sql, SqlParameter[] parameters)
    {
        using var dataReader = _dataReader.GetDataReader(sql, parameters);
        var entity = default(T);
        if (dataReader.HasRows)
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
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    public List<T> SqlQuery<T>(string sql)
    {
        using var dataReader = _dataReader.GetDataReader(sql);
        var entities = new List<T>();
        if (dataReader.HasRows)
        {
            entities = _dataReaderTypeConvert.ToEntities<T>(dataReader);
        }

        return entities;
    }

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
    public List<T> SqlQuery<T>(string sql, List<SqlParameter> parameters)
    {
        return SqlQuery<T>(sql, parameters.ToArray());
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    private List<T> SqlQuery<T>(string sql, SqlParameter[] parameters)
    {
        using var dataReader = _dataReader.GetDataReader(sql, parameters);
        var entities = new List<T>();
        if (dataReader.HasRows)
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
    /// <returns>影响行数</returns>
    public int ExecuteCommand(string sql)
    {
        var command = _command.GetCommand(sql);
        return command.ExecuteNonQuery();
    }

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
        return command.ExecuteNonQuery();
    }

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>影响行数</returns>
    public int ExecuteCommand(string sql, List<SqlParameter> parameters)
    {
        return ExecuteCommand(sql, parameters.ToArray());
    }

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>影响行数</returns>
    private int ExecuteCommand(string sql, SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteNonQuery();
    }

    #endregion

    #region GetScalar

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>首行首列</returns>
    public object GetScalar(string sql)
    {
        var command = _command.GetCommand(sql);
        return command.ExecuteScalar();
    }

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>首行首列</returns>
    public object GetScalar(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return GetScalar(sql, parameters);
    }

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>首行首列</returns>
    public object GetScalar(string sql, List<SqlParameter> parameters)
    {
        return GetScalar(sql, parameters.ToArray());
    }

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>首行首列</returns>
    private object GetScalar(string sql, SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteScalar();
    }

    #endregion

    #region GetDataReader

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>数据读取器</returns>
    public IDataReader GetDataReader(string sql)
    {
        return _dataReader.GetDataReader(sql);
    }

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
    public IDataReader GetDataReader(string sql, List<SqlParameter> parameters)
    {
        return GetDataReader(sql, parameters.ToArray());
    }

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据读取器</returns>
    private IDataReader GetDataReader(string sql, SqlParameter[] parameters)
    {
        return _dataReader.GetDataReader(sql, parameters);
    }

    #endregion

    #region GetDataSet

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>数据结果集</returns>
    public DataSet GetDataSet(string sql)
    {
        var dataAdapter = _dataAdapter.GetDataAdapter();
        var command = _command.GetCommand(sql);
        _dataAdapter.SetCommandToAdapter(dataAdapter, command);
        var dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        return dataSet;
    }

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
    public DataSet GetDataSet(string sql, List<SqlParameter> parameters)
    {
        return GetDataSet(sql, parameters.ToArray());
    }

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据结果集</returns>
    private DataSet GetDataSet(string sql, SqlParameter[] parameters)
    {
        var dataAdapter = _dataAdapter.GetDataAdapter();
        var command = _command.GetCommand(sql, parameters);
        _dataAdapter.SetCommandToAdapter(dataAdapter, command);
        var dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        return dataSet;
    }

    #endregion

    #region GetDataTable

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>数据表格</returns>
    public DataTable GetDataTable(string sql)
    {
        var dataSet = GetDataSet(sql);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
    }

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>数据表格</returns>
    public DataTable GetDataTable(string sql, object parameter)
    {
        var dataSet = GetDataSet(sql, parameter);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
    }

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据表格</returns>
    public DataTable GetDataTable(string sql, List<SqlParameter> parameters)
    {
        return GetDataTable(sql, parameters.ToArray());
    }

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>数据表格</returns>
    private DataTable GetDataTable(string sql, SqlParameter[] parameters)
    {
        var dataSet = GetDataSet(sql, parameters);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
    }

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
    public async Task<T> SqlQuerySingleAsync<T>(string sql, CancellationToken token = default)
    {
        await using var dataReader = await _dataReader.GetDataReaderAsync(sql, token);
        var entity = default(T);
        if (dataReader.HasRows)
        {
            entity = _dataReaderTypeConvert.ToEntity<T>(dataReader);
        }

        return entity;
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    public Task<T> SqlQuerySingleAsync<T>(string sql, object parameter, CancellationToken token = default)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return SqlQuerySingleAsync<T>(sql, parameters, token);
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    public Task<T> SqlQuerySingleAsync<T>(string sql, List<SqlParameter> parameters, CancellationToken token = default)
    {
        return SqlQuerySingleAsync<T>(sql, parameters.ToArray(), token);
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象</returns>
    private async Task<T> SqlQuerySingleAsync<T>(string sql, SqlParameter[] parameters, CancellationToken token = default)
    {
        await using var dataReader = await _dataReader.GetDataReaderAsync(sql, parameters, token);
        var entity = default(T);
        if (dataReader.HasRows)
        {
            entity = _dataReaderTypeConvert.ToEntity<T>(dataReader);
        }

        return entity;
    }

    #endregion

    #region SqlQueryAsync

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    public async Task<List<T>> SqlQueryAsync<T>(string sql, CancellationToken token = default)
    {
        await using var dataReader = await _dataReader.GetDataReaderAsync(sql, token);
        var entities = new List<T>();
        if (dataReader.HasRows)
        {
            entities = _dataReaderTypeConvert.ToEntities<T>(dataReader);
        }

        return entities;
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    public Task<List<T>> SqlQueryAsync<T>(string sql, object parameter, CancellationToken token = default)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return SqlQueryAsync<T>(sql, parameters, token);
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    public Task<List<T>> SqlQueryAsync<T>(string sql, List<SqlParameter> parameters, CancellationToken token = default)
    {
        return SqlQueryAsync<T>(sql, parameters.ToArray(), token);
    }

    /// <summary>
    ///     SQL查询
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    /// <returns>查询结果实体对象列表</returns>
    private async Task<List<T>> SqlQueryAsync<T>(string sql, SqlParameter[] parameters, CancellationToken token = default)
    {
        await using var dataReader = await _dataReader.GetDataReaderAsync(sql, parameters, token);
        var entities = new List<T>();
        if (dataReader.HasRows)
        {
            entities = _dataReaderTypeConvert.ToEntities<T>(dataReader);
        }

        return entities;
    }

    #endregion

    #region ExecuteCommandAsync

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    public Task<int> ExecuteCommandAsync(string sql, CancellationToken token = default)
    {
        var command = _command.GetCommand(sql);
        return command.ExecuteNonQueryAsync(token);
    }

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    public Task<int> ExecuteCommandAsync(string sql, object parameter, CancellationToken token = default)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return ExecuteCommandAsync(sql, parameters, token);
    }

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    public Task<int> ExecuteCommandAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default)
    {
        return ExecuteCommandAsync(sql, parameters.ToArray(), token);
    }

    /// <summary>
    ///     执行SQL
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>影响行数</returns>
    private Task<int> ExecuteCommandAsync(string sql, SqlParameter[] parameters, CancellationToken token = default)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteNonQueryAsync(token);
    }

    #endregion


    #region GetScalarAsync

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <returns>首行首列</returns>
    public Task<object> GetScalarAsync(string sql)
    {
        var command = _command.GetCommand(sql);
        return command.ExecuteScalarAsync();
    }

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>首行首列</returns>
    public Task<object> GetScalarAsync(string sql, object parameter)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return GetScalarAsync(sql, parameters);
    }

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>首行首列</returns>
    public Task<object> GetScalarAsync(string sql, List<SqlParameter> parameters)
    {
        return GetScalarAsync(sql, parameters.ToArray());
    }

    /// <summary>
    ///     获取首行首列
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <returns>首行首列</returns>
    private Task<object> GetScalarAsync(string sql, SqlParameter[] parameters)
    {
        var command = _command.GetCommand(sql, parameters);
        return command.ExecuteScalarAsync();
    }

    #endregion

    #region GetDataReaderAsync

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    public Task<DbDataReader> GetDataReaderAsync(string sql, CancellationToken token = default)
    {
        return _dataReader.GetDataReaderAsync(sql, token);
    }

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    public Task<DbDataReader> GetDataReaderAsync(string sql, object parameter, CancellationToken token = default)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return GetDataReaderAsync(sql, parameters, token);
    }

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    public Task<DbDataReader> GetDataReaderAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default)
    {
        return GetDataReaderAsync(sql, parameters.ToArray(), token);
    }

    /// <summary>
    ///     查询数据读取器
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据读取器</returns>
    private Task<DbDataReader> GetDataReaderAsync(string sql, SqlParameter[] parameters, CancellationToken token = default)
    {
        return _dataReader.GetDataReaderAsync(sql, parameters, token);
    }

    #endregion

    #region GetDataSetAsync

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    public Task<DataSet> GetDataSetAsync(string sql, CancellationToken token = default)
    {
        return Task.Run(async () =>
        {
            var dataSet = new DataSet();
            await using var scopeService = _serviceProvider.CreateAsyncScope();
            var dataAdapter = scopeService.ServiceProvider.GetRequiredService<IDataAdapterProvider>();
            var adapter = dataAdapter.GetDataAdapter();
            var databaseCommand = scopeService.ServiceProvider.GetRequiredService<IDatabaseCommandProvider>();
            var command = databaseCommand.GetCommand(sql);
            dataAdapter.SetCommandToAdapter(adapter, command);
            adapter.Fill(dataSet);
            return dataSet;
        }, token);
    }


    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    public Task<DataSet> GetDataSetAsync(string sql, object parameter, CancellationToken token = default)
    {
        var parameters = _parameterReader.GetSqlParameter(parameter);
        return GetDataSetAsync(sql, parameters, token);
    }


    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    public Task<DataSet> GetDataSetAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default)
    {
        return GetDataSetAsync(sql, parameters.ToArray(), token);
    }

    /// <summary>
    ///     查询数据结果集
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据结果集</returns>
    private Task<DataSet> GetDataSetAsync(string sql, SqlParameter[] parameters, CancellationToken token = default)
    {
        return Task.Run(async () =>
        {
            var dataSet = new DataSet();
            await using var scopeService = _serviceProvider.CreateAsyncScope();
            var dataAdapter = scopeService.ServiceProvider.GetRequiredService<IDataAdapterProvider>();
            var adapter = dataAdapter.GetDataAdapter();
            var databaseCommand = scopeService.ServiceProvider.GetRequiredService<IDatabaseCommandProvider>();
            var command = databaseCommand.GetCommand(sql, parameters);
            dataAdapter.SetCommandToAdapter(adapter, command);
            adapter.Fill(dataSet);
            return dataSet;
        }, token);
    }

    #endregion

    #region GetDataTableAsync

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据表格</returns>
    public async Task<DataTable> GetDataTableAsync(string sql, CancellationToken token = default)
    {
        var dataSet = await GetDataSetAsync(sql, token);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
    }

    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据表格</returns>
    public async Task<DataTable> GetDataTableAsync(string sql, object parameter, CancellationToken token = default)
    {
        var dataSet = await GetDataSetAsync(sql, parameter, token);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
    }


    /// <summary>
    ///     查询数据表格
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="token">取消令牌</param>
    /// <returns>数据表格</returns>
    public async Task<DataTable> GetDataTableAsync(string sql, List<SqlParameter> parameters, CancellationToken token = default)
    {
        var dataSet = await GetDataSetAsync(sql, parameters.ToArray(), token);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
    }

    #endregion

    #endregion
}