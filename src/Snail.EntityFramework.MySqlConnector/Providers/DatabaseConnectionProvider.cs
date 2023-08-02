using System.Data;
using Snail.EntityFramework.Exceptions;
using Snail.EntityFramework.Options;
using Snail.EntityFramework.Utils;
using MySqlConnector;

namespace Snail.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库连接对象提供器
/// </summary>
public class DatabaseConnectionProvider : IDatabaseConnectionProvider
{
    /// <summary>
    ///     数据库连接配置选项提供器
    /// </summary>
    private readonly IDatabaseConnectorOptionsProvider _connectorOptionsProvider;

    /// <summary>
    ///     数据库连接对象
    /// </summary>
    private IDbConnection _connection;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DatabaseConnectionProvider(IDatabaseConnectorOptionsProvider connectorOptionsProvider)
    {
        _connectorOptionsProvider = connectorOptionsProvider;
    }

    /// <summary>
    ///     打开数据库连接
    /// </summary>
    /// <remarks>
    ///     关闭状态（Closed）：重新打开连接
    ///     连接中断（Broken）：关闭当前连接后再重新打开连接
    /// </remarks>
    public void Open()
    {
        try
        {
            switch (_connection.State)
            {
                case ConnectionState.Closed:
                    _connection.Open();
                    break;
                case ConnectionState.Broken:
                    _connection.Close();
                    _connection.Open();
                    break;
            }
        }
        catch (Exception ex)
        {
            throw new EntityFrameworkException(ErrorMessage.ConnectionFailed, ex.Message);
        }
    }

    /// <summary>
    ///     获取数据库连接对象
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <returns>数据库连接对象</returns>
    public IDbConnection GetConnection(string connectionString = default)
    {
        var connectorOptions = _connectorOptionsProvider.GetCurrentConnectorOptions();
        connectionString ??= connectorOptions.ConnectionString;
        if (_connection == null)
        {
            try
            {
                _connection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new EntityFrameworkException(ErrorMessage.ConnectionFailed, ex.Message);
            }
        }

        return _connection;
    }
}