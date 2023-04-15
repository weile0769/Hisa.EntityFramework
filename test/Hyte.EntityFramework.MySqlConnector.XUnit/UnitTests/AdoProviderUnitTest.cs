using System.Data;

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
    ///     打开数据库连接单元测试案例
    /// </summary>
    [Fact(DisplayName = "打开数据库连接单元测试案例")]
    public void ConnectOpenUnitTest()
    {
        try
        {
            _adoProvider.Open();
            Assert.Equal(_adoProvider.Connection.State,ConnectionState.Open);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    /// <summary>
    ///     关闭数据库连接单元测试案例
    /// </summary>
    [Fact(DisplayName = "关闭数据库连接单元测试案例")]
    public void ConnectCloseUnitTest()
    {
        try
        {
            _adoProvider.Close();
            Assert.Equal(_adoProvider.Connection.State,ConnectionState.Closed);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}