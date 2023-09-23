namespace Snail.EntityFramework;

/// <summary>
///     实体框架数据访问提供程序
/// </summary>
public interface ISnailSqlClient
{
    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    public IAdoProvider Ado { get; }
}