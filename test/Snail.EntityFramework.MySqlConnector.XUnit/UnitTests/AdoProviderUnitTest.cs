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

    /// <summary>
    ///     SQL查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL查询单元测试案例")]
    public void SqlQueryUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user
";
        var list = _adoProvider.SqlQuery<User>(sql);
        Assert.NotEmpty(list);
    }
}