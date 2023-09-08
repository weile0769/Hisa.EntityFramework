namespace Snail.EntityFramework.MySqlConnector.XUnit;

/// <summary>
///     数据库配置
/// </summary>
public static class Configure
{
    /// <summary>
    ///     数据库连接标识
    /// </summary>
    public const string ConnectionName = "xunit";
    
    /// <summary>
    ///     数据库连接字符串
    /// </summary>
    public const string ConnectionString = "Server=localhost;Port=3306;database=snail_xunit;uid=root;pwd=123456;SslMode=none;";
}