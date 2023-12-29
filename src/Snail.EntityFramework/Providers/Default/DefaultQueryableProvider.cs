using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器默认实现
/// </summary>
public class DefaultQueryableProvider : IQueryableProvider
{
    private readonly IAdoProvider _adoProvider;

    /// <summary>
    ///     数据参数化提供器
    /// </summary>
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

    public List<string> WhereInfos { get; set; }
    public List<SqlParameter> Parameters { get; set; }

    public IQueryableProvider Where<T>(string sqlWhere, object parameter = null)
    {
        WhereInfos.Add(_sqlBuilderProvider.AppendWhereOrAnd(WhereInfos.Count == 0, sqlWhere));

        if (parameter != null)
        {
            Parameters.AddRange(_parameterReader.GetSqlParameterByObject(parameter));
        }

        return this;
    }

    public List<T> ToList<T>()
    {
        var sql = _queryBuilderProvider.ToSql();
        return _adoProvider.SqlQuery<T>(sql, Parameters);
    }
}