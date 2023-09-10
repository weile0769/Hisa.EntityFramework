using System.Data;
using Snail.EntityFramework.Models;
using Snail.EntityFramework.MySqlConnector.XUnit.Entities;

namespace Snail.EntityFramework.MySqlConnector.XUnit.UnitTests;

/// <summary>
///     AdoProvider单元测试
/// </summary>
[Collection("MySqlConnector数据库驱动测试案例组别")]
public class AdoProviderUnitTest
{
    /// <summary>
    ///     数据库连接对象提供器
    /// </summary>
    private readonly IAdoProvider _adoProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public AdoProviderUnitTest(IAdoProvider adoProvider)
    {
        _adoProvider = adoProvider;
    }

    #region SqlQuery

    /// <summary>
    ///     SQL非参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化列表查询单元测试案例")]
    public void SqlQueryNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>1
";
        var list = _adoProvider.SqlQuery<User>(sql);
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL参数化列表查询单元测试案例")]
    public void SqlQueryIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id
";
        var list = _adoProvider.SqlQuery<User>(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 1
        });
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL对象参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化列表查询单元测试案例")]
    public void SqlQueryIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id and create_time<@createTime
";
        var list = _adoProvider.SqlQuery<User>(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.NotEmpty(list);
    }

    #endregion

    #region SqlQuerySingle

    /// <summary>
    ///     SQL非参数化单实体查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化单实体查询单元测试案例")]
    public void SqlQuerySingleNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var entity = _adoProvider.SqlQuerySingle<User>(sql);
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL参数化单实体查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL参数化单实体查询单元测试案例")]
    public void SqlQuerySingleIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var entity = _adoProvider.SqlQuerySingle<User>(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL对象参数化单实体查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化单实体查询单元测试案例")]
    public void SqlQuerySingleIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var entity = _adoProvider.SqlQuerySingle<User>(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.NotNull(entity);
    }

    #endregion
}