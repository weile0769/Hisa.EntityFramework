namespace Hyte.EntityFramework.MySqlConnector.XUnit.UnitTests;

/// <summary>
///     AdoProvider单元测试
/// </summary>
public class AdoProviderUnitTest
{
    /// <summary>
    ///     数据库访问提供程序
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
    ///     数据库连接单元测试案例
    /// </summary>
    [Fact(DisplayName = "数据库连接单元测试案例")]
    public void ConnectOpenUnitTest()
    {
        var openState = _adoProvider.Open();
        Assert.True(openState);
    }
}