using System.Linq.Expressions;
using Lysa.EntityFramework.Models;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器
/// </summary>
public interface IQueryableProvider<T>
{
    /// <summary>
    ///     查询条件
    /// </summary>
    public List<string> WhereConditions { get; }

    /// <summary>
    ///     查询参数
    /// </summary>
    public List<SqlParameter> SqlParameters { get; }

    /// <summary>
    ///     IQueryable查询对象初始化
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>IQueryable查询对象</returns>
    IQueryableProvider<T> Queryable();

    /// <summary>
    ///     设置查询条件
    /// </summary>
    /// <param name="sqlWhere">查询条件语句</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>IQueryable查询对象提供器</returns>
    IQueryableProvider<T> Where(string sqlWhere, object parameter = null);


    /// <summary>
    ///     设置查询条件
    /// </summary>
    /// <param name="expression">查询条件表达式</param>
    /// <returns>IQueryable查询对象提供器</returns>
    IQueryableProvider<T> Where(Expression<Func<T, bool>> expression);

    /// <summary>
    ///     SQL查询结果集转化实体列表
    /// </summary>
    /// <returns>查询结果实体对象列表</returns>
    List<T> ToList();

    /// <summary>
    ///     SQL查询结果集转化实体列表
    /// </summary>
    /// <returns>查询结果实体对象列表</returns>
    Task<List<T>> ToListAsync();
}