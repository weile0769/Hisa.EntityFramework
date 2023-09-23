namespace Snail.EntityFramework;

/// <summary>
///     实体框架数据访问提供程序
/// </summary>
public class SnailSqlClient : ISnailSqlClient
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public SnailSqlClient(IAdoProvider adoProvider)
    {
        Ado = adoProvider;
    }

    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    public IAdoProvider Ado { get; }
}