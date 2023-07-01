using System.Data;
using Hyte.EntityFramework.Exceptions;

namespace Hyte.EntityFramework.MySqlConnector.XUnit.UnitTests;

/// <summary>
///     DbConnectionProvider单元测试
/// </summary>
[Collection("MySqlConnector数据库驱动测试案例组别")]
public class DbConnectionProviderUnitTest
{
    /// <summary>
    ///     数据库连接对象提供器
    /// </summary>
    private readonly IDatabaseConnectionProvider _connectionProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DbConnectionProviderUnitTest(IDatabaseConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    /// <summary>
    ///     获取数据库连接对象单元测试案例
    /// </summary>
    [Fact(DisplayName = "获取数据库连接对象单元测试案例")]
    public void GetDatabaseConnectionUnitTest()
    {
        var connection = _connectionProvider.GetConnection();
        Assert.NotNull(connection);
    }

    /// <summary>
    ///     数据库连接失败单元测试案例
    /// </summary>
    [Fact(DisplayName = "数据库连接失败单元测试案例")]
    public void DatabaseConnectFailedUnitTest()
    {
        var connectionString = "错误数据库连接字符串";
        _connectionProvider.GetConnection(connectionString);
        Assert.Throws<EntityFrameworkException>(() => { _connectionProvider.CheckConnection(); });
    }

    /// <summary>
    ///     数据库连接成功单元测试案例
    /// </summary>
    [Fact(DisplayName = "数据库连接成功单元测试案例")]
    public void DatabaseConnectSucceedUnitTest()
    {
        var connection = _connectionProvider.GetConnection(Configure.ConnectionString);
        try
        {
            _connectionProvider.CheckConnection();
            Assert.Equal(ConnectionState.Open, connection.State);
        }
        catch (EntityFrameworkException ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}