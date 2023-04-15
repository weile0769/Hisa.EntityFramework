using System.Data;

namespace Hyte.EntityFramework;

/// <summary>
///     数据库访问提供程序
/// </summary>
public interface IAdoProvider
{
    /// <summary>
    ///     数据库连接对象
    /// </summary>
    IDbConnection Connection { get; }

    /// <summary>
    ///     打开数据库连接
    /// </summary>
    /// <returns></returns>
    void Open();

    /// <summary>
    ///     关闭数据库连接
    /// </summary>
    void Close();
}