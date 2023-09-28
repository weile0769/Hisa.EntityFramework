using System.Data;
using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库参数提供器
/// </summary>
public interface IDataParameterProvider
{
    /// <summary>
    ///     获取数据库参数
    /// </summary>
    /// <param name="parameters">自定义参数</param>
    /// <returns>数据库参数</returns>
    IDataParameter[] GetDataParameter(SqlParameter[] parameters);
}