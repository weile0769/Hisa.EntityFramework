using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Extensions.DependencyInjection;
using Snail.EntityFramework.Benchmarks.Entities;

namespace Snail.EntityFramework.Benchmarks.BenchmarkTests;

[SimpleJob(RunStrategy.ColdStart, iterationCount: 10000)]
public class AdoProviderBenchmarkTest
{
    /// <summary>
    ///     数据库连接对象提供器
    /// </summary>
    private readonly IAdoProvider _adoProvider;

    public AdoProviderBenchmarkTest()
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();
        _adoProvider = serviceProvider.GetRequiredService<IAdoProvider>();
    }

    #region SqlQuerySingle

    /// <summary>
    ///     SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterBenchmarkTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        _adoProvider.SqlQuerySingle<User>(sql);
    }

    #endregion
}