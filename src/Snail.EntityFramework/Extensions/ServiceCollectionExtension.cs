using Microsoft.Extensions.Options;
using Snail.EntityFramework;
using Snail.EntityFramework.Builders;
using Snail.EntityFramework.Caching;
using Snail.EntityFramework.Options;
using Snail.EntityFramework.Providers;

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
    public static IEntityFrameworkBuilder AddSnailEntityFramework(this IServiceCollection services, Action<EntityFrameworkOptions> optionAction)
    {
        //配置选注册
        var options = new EntityFrameworkOptions();
        optionAction(options);
        foreach (var serviceExtension in options.Extensions)
        {
            serviceExtension.AddServices(services);
        }

        services.AddMemoryCache();
        services.AddSingleton<ICacheProvider, DefaultCacheProvider>();

        services.Configure(optionAction);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EntityFrameworkOptions>>().Value);
        services.AddSingleton<IDatabaseConnectorOptionsProvider, DatabaseConnectorOptionsProvider>();

        services.AddSingleton<IAdoProvider, DefaultAdoProvider>();
        services.AddSingleton<IDataReaderProvider, DefaultDataReaderProvider>();
        services.AddSingleton<IEntityMappingProvider, DefaultEntityMappingProvider>();
        services.AddTransient(typeof(IDataReaderEntityBuilder<>), typeof(DefaultDataReaderEntityBuilder<>));
        return new EntityFrameworkBuilder(services);
    }
}