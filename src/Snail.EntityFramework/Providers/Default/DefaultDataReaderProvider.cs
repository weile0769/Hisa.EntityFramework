using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Snail.EntityFramework.Builders;
using Snail.EntityFramework.Exceptions;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     数据库读取提供器
/// </summary>
public class DefaultDataReaderProvider : IDataReaderProvider
{
    /// <summary>
    ///     容器服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultDataReaderProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    /// <summary>
    ///     IDataReader对象转换泛型类型实体列表
    /// </summary>
    /// <param name="dataReader"></param>
    /// <typeparam name="T">泛型对象类型</typeparam>
    /// <returns>泛型类型实体列表</returns>
    public List<T> ToEntities<T>(IDataReader dataReader)
    {
        var entities = new List<T>();
        if (dataReader == null)
        {
            return entities;
        }

        var nameTypes = GetDataReaderNameTypes(dataReader);
        var fieldNames = nameTypes.Select(s => s.Key).ToList();
        var entityBuilderFactory = _serviceProvider.GetRequiredService<IDataReaderEntityBuilder<T>>();
        if (entityBuilderFactory == null)
        {
            throw new EntityFrameworkException("实体属性转换器{0}未注册", nameof(IDataReaderEntityBuilder<T>));
        }

        var entityBuilder = entityBuilderFactory.CreateBuilder(dataReader, fieldNames);
        while (dataReader.Read())
        {
            var entity = entityBuilder.Build(dataReader);
            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        return entities;
    }

    /// <summary>
    ///     IDataReader对象转换泛型类型实体
    /// </summary>
    /// <param name="dataReader"></param>
    /// <typeparam name="T">泛型对象类型</typeparam>
    /// <returns>泛型类型实体</returns>
    public T ToEntity<T>(IDataReader dataReader)
    {
        if (dataReader != null)
        {
            var nameTypes = GetDataReaderNameTypes(dataReader);
            var fieldNames = nameTypes.Select(s => s.Key).ToList();
            var entityBuilderFactory = _serviceProvider.GetRequiredService<IDataReaderEntityBuilder<T>>();
            if (entityBuilderFactory == null)
            {
                throw new EntityFrameworkException("实体属性转换器{0}未注册", nameof(IDataReaderEntityBuilder<T>));
            }

            var entityBuilder = entityBuilderFactory.CreateBuilder(dataReader, fieldNames);
            while (dataReader.Read())
            {
                var entity = entityBuilder.Build(dataReader);
                if (entity != null)
                {
                    return entity;
                }
            }
        }

        return default;
    }

    /// <summary>
    ///     获取数据列名称与列类型键值对
    /// </summary>
    /// <param name="dataReader">IDataReader对象</param>
    /// <returns>数据列名称与列类型键值对</returns>
    private IEnumerable<KeyValuePair<string, string>> GetDataReaderNameTypes(IDataRecord dataReader)
    {
        var nameTypes = new List<KeyValuePair<string, string>>();
        for (var i = 0; i < dataReader.FieldCount; i++)
        {
            var name = dataReader.GetName(i);
            var typeName = dataReader.GetFieldType(i).Name;
            nameTypes.Add(new KeyValuePair<string, string>(name, typeName));
        }

        return nameTypes;
    }
}