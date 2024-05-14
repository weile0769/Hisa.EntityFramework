using Lysa.EntityFramework.Exceptions;
using Lysa.EntityFramework.Options;
using Lysa.EntityFramework.Utils;

namespace Lysa.EntityFramework.Providers;

/// <summary>
///     数据库连接配置选项提供器
/// </summary>
public class DefaultDatabaseConnectorOptionsProvider : IDatabaseConnectorOptionsProvider
{
    /// <summary>
    ///     数据库连接配置选项
    /// </summary>
    private readonly EntityFrameworkOptions _options;

    /// <summary>
    ///     数据库配置选项
    /// </summary>
    private DatabaseConfigureOptions _configureOptions;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultDatabaseConnectorOptionsProvider(EntityFrameworkOptions options)
    {
        _options = options;
    }

    /// <summary>
    ///     获取当前数据库连接配置项
    /// </summary>
    /// <returns>当前数据库连接配置项</returns>
    public DatabaseConfigureOptions GetCurrentConnectorOptions()
    {
        if (_configureOptions == null)
        {
            var defaultConfigureOptions = _options.ConfigureOptions.FirstOrDefault(s => s.Enabled && s.Default);

            _configureOptions = defaultConfigureOptions ?? throw new EntityFrameworkException(ErrorMessage.DefaultConnectionFailed);
        }

        return _configureOptions;
    }
}