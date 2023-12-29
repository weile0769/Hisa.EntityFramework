using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器默认实现
/// </summary>
public class DefaultQueryableProvider : IQueryableProvider
{
    private readonly IAdoProvider _adoProvider;
    private readonly ISqlParameterProvider _parameterReader;
    private readonly IQueryBuilderProvider _queryBuilderProvider;
    private readonly ISqlBuilderProvider _sqlBuilderProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultQueryableProvider(IAdoProvider adoProvider,
        ISqlParameterProvider parameterReader,
        ISqlBuilderProvider sqlBuilderProvider,
        IQueryBuilderProvider queryBuilderProvider)
    {
        _adoProvider = adoProvider;
        _parameterReader = parameterReader;
        _sqlBuilderProvider = sqlBuilderProvider;
        _queryBuilderProvider = queryBuilderProvider;
    }

    /// <summary>
    ///     查询条件
    /// </summary>
    public List<string> WhereConditions { get; set; } = new();

    /// <summary>
    ///     查询参数
    /// </summary>
    public List<SqlParameter> SqlParameters { get; set; } = new();

    /// <summary>
    ///     设置查询条件
    /// </summary>
    /// <param name="sqlWhere">查询条件语句</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>IQueryable查询对象提供器</returns>
    public IQueryableProvider Where<T>(string sqlWhere, object parameter = null)
    {
        WhereConditions.Add(_sqlBuilderProvider.AppendWhereOrAnd(WhereConditions.Count == 0, sqlWhere));

        if (parameter != null)
        {
            SqlParameters.AddRange(_parameterReader.GetSqlParameterByObject(parameter));
        }

        return this;
    }

    public List<T> ToList<T>()
    {
        var sql = _queryBuilderProvider.ToSql();
        return _adoProvider.SqlQuery<T>(sql, SqlParameters);
    }
}