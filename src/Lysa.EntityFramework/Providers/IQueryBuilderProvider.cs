namespace Lysa.EntityFramework.Providers;

/// <summary>
///     IQueryable查询对象提供器
/// </summary>
public interface IQueryBuilderProvider
{
    /// <summary>
    ///     实体类型
    /// </summary>
    public Type EntityType { get; set; }

    /// <summary>
    ///     根据预设SQL模版转换SQL
    /// </summary>
    /// <returns>SQL脚本</returns>
    string ToSql();
}