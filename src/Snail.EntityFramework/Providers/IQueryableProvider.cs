using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器
/// </summary>
public interface IQueryableProvider
{
    public List<string> WhereInfos { get; set; }
    public List<SqlParameter> Parameters { get; set; }

    IQueryableProvider Where<T>(string sqlWhere, object parameter = null);

    List<T> ToList<T>();
}