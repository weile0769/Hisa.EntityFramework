namespace Hyte.EntityFramework.Utils;

/// <summary>
///     错误消息模板
/// </summary>
public class ErrorMessage
{
    /// <summary>
    ///     连接失败
    /// </summary>
    public const string ConnectionFailed = "连接数据库过程中发生错误，检查数据库连接字符串是否配置正确，错误信息：{0}.";
    
    /// <summary>
    ///     数据库配置失败
    /// </summary>
    public const string DefaultConnectionFailed = "获取数据库配置项过程中发生错误，检查DatabaseConfigureOptions默认配置项是否正确.";
}