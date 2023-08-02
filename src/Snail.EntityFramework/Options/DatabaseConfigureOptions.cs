namespace Snail.EntityFramework.Options;

/// <summary>
///     数据库配置选项
/// </summary>
public class DatabaseConfigureOptions
{
    /// <summary>
    ///     是否开启
    /// </summary>
    /// <remarks>
    ///     默认：true
    ///     说明：
    ///     是：true
    ///     否：false
    /// </remarks>
    public bool Enabled { get; set; } = true;

    /// <summary>
    ///     是否默认数据库
    /// </summary>
    /// <remarks>
    ///     默认：false
    ///     说明：
    ///     是：true
    ///     否：false
    /// </remarks>
    public bool Default { get; set; } = false;

    /// <summary>
    ///     数据库连接对象标识
    /// </summary>
    public string ConnectionName { get; set; }

    /// <summary>
    ///     数据库连接字符串
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    ///     数据库命令执行等待时间
    /// </summary>
    /// <remarks>
    ///     默认值：300秒
    /// </remarks>
    public int CommandTimeOut { get; set; } = 300;
}