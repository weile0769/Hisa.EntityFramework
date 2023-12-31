using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework;

/// <summary>
///     实体框架数据访问提供程序
/// </summary>
public interface ISqlClient
{
    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    public IAdoProvider Ado { get; }

    /// <summary>
    ///     IQueryable查询对象初始化
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>IQueryable查询对象</returns>
    IQueryableProvider<T> Queryable<T>();
}