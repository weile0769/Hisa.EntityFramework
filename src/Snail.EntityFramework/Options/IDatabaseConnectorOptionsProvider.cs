namespace Snail.EntityFramework.Options;

/// <summary>
///     数据库连接配置选项提供器
/// </summary>
public interface IDatabaseConnectorOptionsProvider
{
    /// <summary>
    ///     获取当前数据库连接配置项
    /// </summary>
    /// <returns>当前数据库连接配置项</returns>
    DatabaseConfigureOptions GetCurrentConnectorOptions();
}