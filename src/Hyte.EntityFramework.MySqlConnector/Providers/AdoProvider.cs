using System.Data;
using Hyte.EntityFramework.Options;
using Hyte.EntityFramework.Utilities;
using MySqlConnector;

namespace Hyte.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据库访问提供程序默认实现
/// </summary>
public class AdoProvider : IAdoProvider
{
    private readonly MySqlConnectorOptions _entityFrameworkOptions;
    private IDbConnection _connection;

    public AdoProvider(MySqlConnectorOptions entityFrameworkOptions)
    {
        _entityFrameworkOptions = entityFrameworkOptions;
    }

    /// <summary>
    ///     数据库连接对象
    /// </summary>
    public IDbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                try
                {
                    _connection = new MySqlConnection(_entityFrameworkOptions.ConnectionString);
                }
                catch (Exception ex)
                {
                    Check.Exception(ex.Message, ex);
                }
            }

            return _connection;
        }
    }

    /// <summary>
    ///     打开数据库连接
    /// </summary>
    public void Open()
    {
        if (Connection.State != ConnectionState.Open)
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                Check.Exception(ErrorMessage.ConnectionFailed, ex.Message);
            }
        }
    }
}