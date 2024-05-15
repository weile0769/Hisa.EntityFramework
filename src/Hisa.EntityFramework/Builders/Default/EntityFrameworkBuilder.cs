using Microsoft.Extensions.DependencyInjection;

namespace Hisa.EntityFramework.Builders;

/// <summary>
///     实体框架构造器
/// </summary>
internal class EntityFrameworkBuilder : IEntityFrameworkBuilder
{
    /// <summary>
    ///     构造函数
    /// </summary>
    internal EntityFrameworkBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    /// <summary>
    ///     容器服务提供器
    /// </summary>
    public IServiceCollection Services { get; }
}