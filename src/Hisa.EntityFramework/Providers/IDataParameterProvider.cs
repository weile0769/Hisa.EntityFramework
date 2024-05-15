using System.Data;
using Hisa.EntityFramework.Models;

namespace Hisa.EntityFramework.Providers;

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