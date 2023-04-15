using Microsoft.Extensions.DependencyInjection;

namespace Hyte.EntityFramework.Builders;

/// <summary>
///     实体框架构造器
/// </summary>
public interface IEntityFrameworkBuilder
{
    /// <summary>
    ///     容器服务提供器
    /// </summary>
    IServiceCollection Services { get; }
}