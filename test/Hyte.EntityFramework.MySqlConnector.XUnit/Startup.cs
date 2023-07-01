using Hyte.EntityFramework.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Hyte.EntityFramework.MySqlConnector.XUnit;

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
        services.AddHyteEntityFramework(options =>
        {
            options.ConfigureOptions = new List<DatabaseConfigureOptions>
            {
                new DatabaseConfigureOptions
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