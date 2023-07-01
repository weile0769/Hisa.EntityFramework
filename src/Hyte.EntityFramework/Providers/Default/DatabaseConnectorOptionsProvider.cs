using Hyte.EntityFramework.Options;
using Hyte.EntityFramework.Utils;

namespace Hyte.EntityFramework.Providers;

/// <summary>
///     数据库连接配置选项提供器
/// </summary>
public class DatabaseConnectorOptionsProvider : IDatabaseConnectorOptionsProvider
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
    public DatabaseConnectorOptionsProvider(EntityFrameworkOptions options)
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
            if (defaultConfigureOptions == null)
            {
                FrameCheck.Exception(ErrorMessage.DefaultConnectionFailed);
            }

            _configureOptions = defaultConfigureOptions;
        }

        return _configureOptions;
    }
}