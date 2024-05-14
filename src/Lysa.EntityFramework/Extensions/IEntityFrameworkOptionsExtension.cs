namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     实体框架配置选项扩展类
/// </summary>
public interface IEntityFrameworkOptionsExtension
{
    /// <summary>
    ///     注册容器服务
    /// </summary>
    /// <param name="services">服务容器</param>
    void AddServices(IServiceCollection services);
}