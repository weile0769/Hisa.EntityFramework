using Hisa.EntityFramework.Options;
using Lysa.EntityFramework.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Hisa.EntityFramework.XUnit;

/// <summary>
///     项目启动配置类
/// </summary>
public class Startup
{
    /// <summary>
    ///     配置服务
    /// </summary>
    /// <param name="services">容器服务</param>
    public void ConfigureServices(IServiceCollection services)
    {
        //注册数据库实体框架
        services.AddHisaSqlEntityFramework(options =>
        {
            options.ConfigureOptions = new List<DatabaseConfigureOptions>
            {
                new()
                {
                    Enabled = true,
                    Default = true,
                    ConnectionName = Configure.ConnectionName,
                    ConnectionString = Configure.ConnectionString
                }
            };

            options.UseMySqlConnector();
        });
    }
}