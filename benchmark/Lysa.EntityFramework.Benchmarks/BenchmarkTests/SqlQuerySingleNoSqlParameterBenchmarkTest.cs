using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Lysa.EntityFramework.Benchmarks.Entities;
using Lysa.EntityFramework.Providers;
using SqlSugar;

namespace Lysa.EntityFramework.Benchmarks.BenchmarkTests;

/// <summary>
///     SQL非参数化单实体查询性能测试案例
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class SqlQuerySingleNoSqlParameterBenchmarkTest
{
    /// <summary>
    ///     LysaSql数据库框架
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
    public SqlQuerySingleNoSqlParameterBenchmarkTest()
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
    [Params(1)]
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
    ///     LysaEntityFramework-SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterForLysaEntityFrameworkBenchmarkTest()
    {
        var sql = $@"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id={pageSize}
";
        _adoProvider.SqlQuerySingle<User>(sql);
    }

    /// <summary>
    ///     SqlSugar-SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterForSqlSugarBenchmarkTest()
    {
        var sql = $@"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id={pageSize}
";
        _sqlSugarClient.Ado.SqlQuerySingle<User>(sql);
    }

    /// <summary>
    ///     FreeSql-SQL非参数化单实体查询性能测试案例
    /// </summary>
    [Benchmark]
    public void SqlQuerySingleNoSqlParameterForFreeSqlBenchmarkTest()
    {
        var sql = $@"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id={pageSize}
";
        _freeSqlClient.Ado.QuerySingle<User>(sql);
    }

    #endregion
}