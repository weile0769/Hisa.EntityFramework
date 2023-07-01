using System.Data;
using Hyte.EntityFramework.Models;

namespace Hyte.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     数据读取提供器
/// </summary>
public class DataReaderProvider:IDataReaderProvider
{
    /// <summary>
    ///     数据库命令提供器
    /// </summary>
    private readonly IDatabaseCommandProvider _databaseCommandProvider;
    
    /// <summary>
    ///     构造函数
    /// </summary>
    public DataReaderProvider(IDatabaseCommandProvider databaseCommandProvider)
    {
        _databaseCommandProvider = databaseCommandProvider;
    }

    public virtual IDataReader GetDataReader(string sql, params SqlParameter[] parameters)
    {
        using var dbCommand = _databaseCommandProvider.GetCommand(sql,parameters);
        return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
    }
}