using System.Data;

namespace Hisa.EntityFramework.Providers;

/// <summary>
///     数据库读取转换泛型实体提供器
/// </summary>
public interface IDataReaderTypeConvertProvider
{
    /// <summary>
    ///     IDataReader对象转换泛型类型实体
    /// </summary>
    /// <param name="dataReader"></param>
    /// <typeparam name="T">泛型对象类型</typeparam>
    /// <returns>泛型类型实体</returns>
    T ToEntity<T>(IDataReader dataReader);

    /// <summary>
    ///     IDataReader对象转换泛型类型实体列表
    /// </summary>
    /// <param name="dataReader"></param>
    /// <typeparam name="T">泛型对象类型</typeparam>
    /// <returns>泛型类型实体列表</returns>
    List<T> ToEntities<T>(IDataReader dataReader);
}