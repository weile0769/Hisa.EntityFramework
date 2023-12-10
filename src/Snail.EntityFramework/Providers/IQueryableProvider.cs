using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器
/// </summary>
public interface IQueryableProvider<T>
{
    public List<string> WhereInfos { get; set; }
    public List<SqlParameter> Parameters { get; set; }

    IQueryableProvider<T> Where<T>(string sqlWhere, object parameter = null);
}