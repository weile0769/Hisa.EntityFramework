using System.Data;
using Hisa.EntityFramework.Builders;
using Hisa.EntityFramework.Providers;
using Microsoft.Extensions.Logging;

namespace Hisa.EntityFramework.Caching;

public class CachingDataReaderEntityBuilder<T> : IDataReaderEntityBuilder<T>
{
    /// <summary>
    ///     缓存键
    /// </summary>
    private const string CacheKey = "hisa:reader:entity:builder:{0}";

    /// <summary>
    ///     缓存服务
    /// </summary>
    private readonly ICacheProvider _cacheProvider;

    /// <summary>
    ///     实体属性转换器
    /// </summary>
    private readonly IDataReaderEntityBuilder<T> _dataReaderEntityBuilder;

    /// <summary>
    ///     日志服务
    /// </summary>
    private readonly ILogger<CachingDataReaderEntityBuilder<T>> _logger;

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <returns></returns>
    public CachingDataReaderEntityBuilder(ICacheProvider cacheProvider,
        DataReaderEntityBuilder<T> dataReaderEntityBuilder,
        ILogger<CachingDataReaderEntityBuilder<T>> logger)
    {
        _logger = logger;
        _cacheProvider = cacheProvider;
        _dataReaderEntityBuilder = dataReaderEntityBuilder;
    }

    /// <summary>
    ///     执行Load委托函数
    /// </summary>
    /// <param name="dataRecord">IDataRecord访问器</param>
    /// <returns>实体</returns>
    public T Build(IDataRecord dataRecord)
    {
        return _dataReaderEntityBuilder.Build(dataRecord);
    }

    /// <summary>
    ///     创建数据列转换映射实体属性构造器
    /// </summary>
    /// <param name="dataRecord">IDataRecord访问器</param>
    /// <param name="readerNameKeys"></param>
    /// <returns>数据列转换映射实体属性构造器</returns>
    public IDataReaderEntityBuilder<T> CreateBuilder(IDataRecord dataRecord, List<KeyValuePair<string, string>> readerNameKeys)
    {
        var fullName = typeof(T).FullName;
        var fieldNames = readerNameKeys.Select(s => s.Key);
        var typeNames = readerNameKeys.Select(s => s.Key);
        var builder = _cacheProvider.Get(string.Format(CacheKey, $"CreateBuild.{fullName}{typeNames.ForeachSelectString()}{fieldNames.ForeachSelectString()}"),
            TimeSpan.FromDays(366),
            () => _dataReaderEntityBuilder.CreateBuilder(dataRecord, readerNameKeys),
            _logger);

        return builder;
    }
}