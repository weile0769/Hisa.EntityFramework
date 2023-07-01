namespace Hyte.EntityFramework.Options;

/// <summary>
///     数据库配置选项
/// </summary>
public class MySqlConnectorOptions
{
    /// <summary>
    ///     数据库连接字符串
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    ///     数据库命令执行等待时间
    /// </summary>
    public int CommandTimeOut { get; set; }
}