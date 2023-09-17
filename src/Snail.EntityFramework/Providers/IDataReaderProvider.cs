using System.Data;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework;

/// <summary>
///     数据库读取提供器
/// </summary>
public interface IDataReaderProvider
{
    /// <summary>
    ///     获取数据读取器对象
    /// </summary>
    /// <param name="sql">SQL脚本</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    IDataReader GetDataReader(string sql, params SqlParameter[] parameters);
}