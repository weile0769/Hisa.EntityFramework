using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Extensions.DependencyInjection;
using Snail.EntityFramework.Benchmarks.Entities;
using SqlSugar;

namespace Snail.EntityFramework.Benchmarks.BenchmarkTests;

[MinColumn]
[Q1Column]
[Q3Column]
[MaxColumn]
[RankColumn]
[SimpleJob(RunStrategy.ColdStart, iterationCount: 10000)]
public class AdoProviderBenchmarkTest
{
    /// <summary>
    ///     SnailSql数据库框架
    /// </summary>
    private readonly IAdoProvider _adoProvider;

    /// <summary>
    ///     FreeSql数据库框架
    /// </summary>
    private readonly IFreeSql _freeSqlClient;

    /// <summary>
    ///     SqlSugar数据库框架
    /// </summary>
    private readonly ISqlSugarClient _sqlSugarClient;

    /// <summary>
    ///     构造函数
    /// </summary>
    public AdoProviderBenchmarkTest()
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();
        _adoProvider = serviceProvider.GetRequiredService<IAdoProvider>();
        _sqlSugarClient = serviceProvider.GetRequiredService<ISqlSugarClient>();
        _freeSqlClient = serviceProvider.GetRequiredService<IFreeSql>();
    }

    #region SqlQuerySingle

    /// <summary>
    ///     SnailEntityFramework-SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterForSnailEntityFrameworkBenchmarkTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        _adoProvider.SqlQuerySingle<User>(sql);
    }

    /// <summary>
    ///     SqlSugar-SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterForSqlSugarBenchmarkTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        _sqlSugarClient.Ado.SqlQuerySingle<User>(sql);
    }

    /// <summary>
    ///     FreeSql-SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterForFreeSqlBenchmarkTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        _freeSqlClient.Ado.QuerySingle<User>(sql);
    }

    #endregion
}