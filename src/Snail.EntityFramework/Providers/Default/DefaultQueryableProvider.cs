using System.Linq.Expressions;
using Snail.EntityFramework.Expressions;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器默认实现
/// </summary>
public class DefaultQueryableProvider<T> : IQueryableProvider<T>
{
    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    private readonly IAdoProvider _adoProvider;

    /// <summary>
    ///     Lambda表达树解析器接口
    /// </summary>
    private readonly ILambdaExpressionProvider _expressionProvider;

    /// <summary>
    ///     数据参数化提供器
    /// </summary>
    private readonly ISqlParameterProvider _parameterReader;

    /// <summary>
    ///     IQueryable查询对象提供器
    /// </summary>
    private readonly IQueryBuilderProvider _queryBuilderProvider;

    /// <summary>
    ///     SQL构造器
    /// </summary>
    private readonly ISqlBuilderProvider _sqlBuilderProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultQueryableProvider(IAdoProvider adoProvider,
        ISqlParameterProvider parameterReader,
        ISqlBuilderProvider sqlBuilderProvider,
        IQueryBuilderProvider queryBuilderProvider,
        ILambdaExpressionProvider expressionProvider)
    {
        _adoProvider = adoProvider;
        _parameterReader = parameterReader;
        _sqlBuilderProvider = sqlBuilderProvider;
        _expressionProvider = expressionProvider;
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
    ///     IQueryable查询对象初始化
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>IQueryable查询对象</returns>
    public IQueryableProvider<T> Queryable()
    {
        _queryBuilderProvider.EntityType = typeof(T);
        return this;
    }

    /// <summary>
    ///     设置查询条件
    /// </summary>
    /// <param name="sqlWhere">查询条件语句</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>IQueryable查询对象提供器</returns>
    public IQueryableProvider<T> Where(string sqlWhere, object parameter = null)
    {
        WhereConditions.Add(_sqlBuilderProvider.AppendWhereOrAnd(WhereConditions.Count == 0, sqlWhere));

        if (parameter != null)
        {
            SqlParameters.AddRange(_parameterReader.GetSqlParameterByObject(parameter));
        }

        return this;
    }

    /// <summary>
    ///     SQL查询结果集转化实体列表
    /// </summary>
    /// <returns>查询结果实体对象列表</returns>
    public List<T> ToList()
    {
        var sql = _queryBuilderProvider.ToSql();
        return SqlParameters.HasValue() ? _adoProvider.SqlQuery<T>(sql, SqlParameters) : _adoProvider.SqlQuery<T>(sql);
    }

    /// <summary>
    ///     SQL查询结果集转化实体列表
    /// </summary>
    /// <returns>查询结果实体对象列表</returns>
    public Task<List<T>> ToListAsync()
    {
        var sql = _queryBuilderProvider.ToSql();
        return SqlParameters.HasValue() ? _adoProvider.SqlQueryAsync<T>(sql, SqlParameters) : _adoProvider.SqlQueryAsync<T>(sql);
    }

    /// <summary>
    ///     设置查询条件
    /// </summary>
    /// <param name="expression">查询条件表达式</param>
    /// <returns>IQueryable查询对象提供器</returns>
    public IQueryableProvider<T> Where(Expression<Func<T, bool>> expression)
    {
        var sqlWhere = _expressionProvider.ResolveWhereLambdaExpression(expression);

        WhereConditions.Add(_sqlBuilderProvider.AppendWhereOrAnd(WhereConditions.Count == 0, sqlWhere));

        return this;
    }
}