using Snail.EntityFramework;
using Snail.EntityFramework.Builders;
using Snail.EntityFramework.Options;
using Snail.EntityFramework.Providers;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     IServiceCollection服务容器扩展类
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    ///     注册数据库实体框架
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="optionAction">配置选项</param>
    /// <returns></returns>
    public static IEntityFrameworkBuilder AddHyteEntityFramework(this IServiceCollection services, Action<EntityFrameworkOptions> optionAction)
    {
        //配置选注册
        var options = new EntityFrameworkOptions();
        optionAction(options);
        foreach (var serviceExtension in options.Extensions)
        {
            serviceExtension.AddServices(services);
        }

        services.Configure(optionAction);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EntityFrameworkOptions>>().Value);
        services.AddSingleton<IDatabaseConnectorOptionsProvider, DatabaseConnectorOptionsProvider>();
        return new EntityFrameworkBuilder(services);
    }
}