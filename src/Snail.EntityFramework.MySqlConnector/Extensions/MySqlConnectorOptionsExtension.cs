using Snail.EntityFramework.MySqlConnector.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Snail.EntityFramework.Options;

/// <summary>
///     MySqlConnector数据库配置选项扩展
/// </summary>
public class MySqlConnectorOptionsExtension : IEntityFrameworkOptionsExtension
{
    /// <summary>
    ///     配置选项
    /// </summary>
    private readonly Action<MySqlConnectorOptions> _optionAction;

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="optionAction">配置选项</param>
    public MySqlConnectorOptionsExtension(Action<MySqlConnectorOptions> optionAction)
    {
        _optionAction = optionAction;
    }

    /// <summary>
    ///     添加服务
    /// </summary>
    /// <param name="services">服务容器</param>
    public void AddServices(IServiceCollection services)
    {
        if (_optionAction != null)
        {
            services.Configure(_optionAction);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MySqlConnectorOptions>>().Value);
        }

        services.AddScoped<IDatabaseConnectionProvider, DatabaseConnectionProvider>();
        services.AddTransient<IDataParameterProvider, DataParameterProvider>();
        services.AddTransient<IDatabaseCommandProvider, DatabaseCommandProvider>();
        services.AddTransient<IAdoProvider, AdoProvider>();
    }
}