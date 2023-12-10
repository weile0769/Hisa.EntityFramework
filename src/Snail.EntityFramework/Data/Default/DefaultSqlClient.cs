using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework;

/// <summary>
///     实体框架数据访问提供程序
/// </summary>
public class DefaultSqlClient : ISqlClient
{
    /// <summary>
    ///     IQueryable查询对象提供器
    /// </summary>
    private readonly IQueryableProvider _queryableProvider;
    
    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultSqlClient(IAdoProvider adoProvider,
        IQueryableProvider queryableProvider)
    {
        Ado = adoProvider;
        _queryableProvider = queryableProvider;
    }

    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    public IAdoProvider Ado { get; }
    
    /*public IQueryableProvider<T> Queryable<T>()
    {
        return _queryableProvider.Queryable<T>();
    }*/
}