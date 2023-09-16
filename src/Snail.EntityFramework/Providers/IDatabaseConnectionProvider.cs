using System.Data;

namespace Snail.EntityFramework;

/// <summary>
///     数据库连接对象提供器
/// </summary>
public interface IDatabaseConnectionProvider
{
    /// <summary>
    ///     获取数据库连接对象
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <returns>数据库连接对象</returns>
    IDbConnection GetConnection(string connectionString = default);

    /// <summary>
    ///     检查数据库连接状态
    /// </summary>
    /// <remarks>
    ///     关闭状态（Closed）：重新打开连接
    ///     连接中断（Broken）：关闭当前连接后再重新打开连接
    /// </remarks>
    void Open();
}