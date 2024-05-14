using Microsoft.Extensions.DependencyInjection;
using Lysa.EntityFramework.Providers;

namespace Lysa.EntityFramework;

/// <summary>
///     实体框架数据访问提供程序
/// </summary>
public class DefaultSqlClient : ISqlClient
{
    /// <summary>
    ///     容器服务对象提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultSqlClient(IAdoProvider adoProvider,
        IServiceProvider serviceProvider)
    {
        Ado = adoProvider;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    public IAdoProvider Ado { get; }

    /// <summary>
    ///     IQueryable查询对象初始化
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>IQueryable查询对象</returns>
    public IQueryableProvider<T> Queryable<T>()
    {
        var queryableProvider = _serviceProvider.GetRequiredService<IQueryableProvider<T>>();
        return queryableProvider.Queryable();
    }
}