namespace Hyte.EntityFramework;

/// <summary>
///     数据库访问提供程序
/// </summary>
public interface IAdoProvider
{
    /// <summary>
    ///     打开数据库连接
    /// </summary>
    /// <returns></returns>
    public void Open();
}