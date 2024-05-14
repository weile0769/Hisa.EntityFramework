using FreeSql;
using Lysa.EntityFramework.Benchmarks;
using Lysa.EntityFramework.Options;
using SqlSugar;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     项目启动配置类
/// </summary>
public static class Startup
{
    /// <summary>
    ///     配置服务
    /// </summary>
    /// <param name="services">容器服务</param>
    public static void ConfigureServices(this IServiceCollection services)
    {
        //注册数据库实体框架
        services.AddLysaSqlEntityFramework(options =>
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

        services.AddScoped<ISqlSugarClient>(s =>
        {
            var sqlSugar = new SqlSugarClient(new ConnectionConfig
            {
                DbType = DbType.MySql,
                ConnectionString = Configure.ConnectionString,
                IsAutoCloseConnection = true
            });
            return sqlSugar;
        });


        services.AddSingleton(r => new FreeSqlBuilder()
            .UseConnectionString(DataType.MySql, Configure.ConnectionString)
            .UseAutoSyncStructure(false)
            .UseNoneCommandParameter(true)
            .Build());

        services.AddLogging();
    }
}