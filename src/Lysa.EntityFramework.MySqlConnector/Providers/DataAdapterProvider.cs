using System.Data;
using MySqlConnector;
using Lysa.EntityFramework.Providers;

namespace Lysa.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据读取适配器
/// </summary>
public class DataAdapterProvider : IDataAdapterProvider
{
    /// <summary>
    ///     创建并获取数据读取适配器
    /// </summary>
    /// <returns>数据读取适配器</returns>
    public IDataAdapter GetDataAdapter()
    {
        return new MySqlDataAdapter();
    }

    /// <summary>
    ///     传递数据库命令到适配器上
    /// </summary>
    /// <param name="dataAdapter">数据读取适配器</param>
    /// <param name="command">数据库命令提供器</param>
    public void SetCommandToAdapter(IDataAdapter dataAdapter, IDbCommand command)
    {
        ((MySqlDataAdapter)dataAdapter).SelectCommand = (MySqlCommand)command;
    }
}