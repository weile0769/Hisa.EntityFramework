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
            options.UseMySqlConnector(connectOptions =>
            {
                connectOptions.ConnectionString =
                    "Server=172.17.250.112;Port=3307;database=erp_scm;uid=root;pwd=jianke@20180329;SslMode=none;Convert Zero Datetime=True;AllowUserVariables=True;AllowLoadLocalInfile=true";
            });
        });
    }
}