using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Snail.EntityFramework.Benchmarks.Entities;
using Snail.EntityFramework.Providers;
using SqlSugar;

namespace Snail.EntityFramework.Benchmarks.BenchmarkTests;

/// <summary>
///     SQL非参数化列表查询性能测试案例
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class SqlQueryNoSqlParameterBenchmarkTest
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
    public SqlQueryNoSqlParameterBenchmarkTest()
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();
        _adoProvider = serviceProvider.GetRequiredService<IAdoProvider>();
        _sqlSugarClient = serviceProvider.GetRequiredService<ISqlSugarClient>();
        _freeSqlClient = serviceProvider.GetRequiredService<IFreeSql>();
    }


    /// <summary>
    ///     定义不同规模的数据作为参数
    /// </summary>
    [Params(1, 10, 100, 200, 500, 1000)]
    public int pageSize { get; set; }

    /// <summary>
    ///     初始化程序
    /// </summary>
    [GlobalSetup]
    public void BenchmarkTestSetup()
    {
        _sqlSugarClient.DbMaintenance.TruncateTable<User>();
        var userModels = new List<User>();
        for (var i = 1; i <= 2000; i++)
        {
            userModels.Add(new User
            {
                Id = i,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            });
        }

        _sqlSugarClient.Insertable(userModels).ExecuteCommand();
    }


    #region SqlQuerySingle

    /// <summary>
    ///     SnailEntityFramework-SQL非参数化列表查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest()
    {
        var sql = $@"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user
limit 0,{pageSize}
";
        _adoProvider.SqlQuery<User>(sql);
    }

    /// <summary>
    ///     SqlSugar-SQL非参数化单列表查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQueryNoSqlParameterForSqlSugarBenchmarkTest()
    {
        var sql = $@"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user
limit 0,{pageSize}
";
        _sqlSugarClient.Ado.SqlQuery<User>(sql);
    }

    /// <summary>
    ///     FreeSql-SQL非参数化单列表查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQueryNoSqlParameterForFreeSqlBenchmarkTest()
    {
        var sql = $@"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user
limit 0,{pageSize}
";
        _freeSqlClient.Ado.Query<User>(sql);
    }

    #endregion
}