using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器默认实现
/// </summary>
public class DefaultQueryableProvider<T> : IQueryableProvider<T>
{
    private readonly IAdoProvider _adoProvider;
    
    /// <summary>
    ///     数据参数化提供器
    /// </summary>
    private readonly ISqlParameterProvider _parameterReader;

    private readonly ISqlBuilderProvider _sqlBuilderProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultQueryableProvider(IAdoProvider adoProvider,
        ISqlParameterProvider parameterReader,
        ISqlBuilderProvider sqlBuilderProvider)
    {
        _adoProvider = adoProvider;
        _parameterReader = parameterReader;
        _sqlBuilderProvider = sqlBuilderProvider;
    }

    public List<string> WhereInfos { get; set; }
    public List<SqlParameter> Parameters { get; set; }
    
    public IQueryableProvider<T> Where(string sqlWhere, object parameter = null)
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
        List<T> result = null;
        return this;
    }
}