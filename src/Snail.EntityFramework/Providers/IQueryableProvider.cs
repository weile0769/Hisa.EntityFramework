using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器
/// </summary>
public interface IQueryableProvider
{
    /// <summary>
    ///     查询条件
    /// </summary>
    public List<string> WhereConditions { get; set; }

    /// <summary>
    ///     查询参数
    /// </summary>
    public List<SqlParameter> SqlParameters { get; set; }

    /// <summary>
    ///     设置查询条件
    /// </summary>
    /// <param name="sqlWhere">查询条件语句</param>
    /// <param name="parameter">查询参数</param>
    /// <returns>IQueryable查询对象提供器</returns>
    IQueryableProvider Where<T>(string sqlWhere, object parameter = null);

    List<T> ToList<T>();
}