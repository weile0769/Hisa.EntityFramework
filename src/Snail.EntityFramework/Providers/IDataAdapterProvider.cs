using System.Data;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据读取适配器
/// </summary>
public interface IDataAdapterProvider
{
    /// <summary>
    ///     创建并获取数据读取适配器
    /// </summary>
    /// <returns>数据读取适配器</returns>
    IDataAdapter GetDataAdapter();

    /// <summary>
    ///     传递数据库命令到适配器上
    /// </summary>
    /// <param name="dataAdapter">数据读取适配器</param>
    /// <param name="command">数据库命令提供器</param>
    void SetCommandToAdapter(IDataAdapter dataAdapter, IDbCommand command);
}