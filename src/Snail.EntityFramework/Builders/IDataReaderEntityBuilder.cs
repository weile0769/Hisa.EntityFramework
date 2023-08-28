using System.Data;

namespace Snail.EntityFramework.Builders;

/// <summary>
///     IDataRecord数据列转换映射实体属性构造器
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public interface IDataReaderEntityBuilder<out T>
{
    /// <summary>
    ///     执行Load委托函数
    /// </summary>
    /// <param name="dataRecord">IDataRecord访问器</param>
    /// <returns>实体</returns>
    T Build(IDataRecord dataRecord);

    /// <summary>
    ///     创建数据列转换映射实体属性构造器
    /// </summary>
    /// <param name="dataRecord">IDataRecord访问器</param>
    /// <param name="readerKeys"></param>
    /// <returns></returns>
    /// <exception cref="EntityFrameworkException"></exception>
    void CreateEntityBuilder(IDataRecord dataRecord, List<string> readerKeys);
}