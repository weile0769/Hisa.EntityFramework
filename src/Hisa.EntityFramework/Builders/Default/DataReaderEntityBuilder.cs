using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using Hisa.EntityFramework.Exceptions;
using Hisa.EntityFramework.Models;
using Hisa.EntityFramework.Providers;
using Hisa.EntityFramework.Utils;

namespace Hisa.EntityFramework.Builders;

/// <summary>
///     实体属性转换器
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public class DataReaderEntityBuilder<T> : IDataReaderEntityBuilder<T>
{
    /// <summary>
    ///     实体映射提供者
    /// </summary>
    private readonly IEntityMappingProvider _entityMapping;

    /// <summary>
    ///     委托函数Load
    /// </summary>
    private Load _handler;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DataReaderEntityBuilder(IEntityMappingProvider entityMapping)
    {
        _entityMapping = entityMapping;
    }

    /// <summary>
    ///     执行Load委托函数
    /// </summary>
    /// <param name="dataRecord">IDataRecord访问器</param>
    /// <returns>实体</returns>
    public T Build(IDataRecord dataRecord)
    {
        return _handler(dataRecord);
    }

    /// <summary>
    ///     创建数据列转换映射实体属性构造器
    /// </summary>
    /// <param name="dataRecord">IDataRecord访问器</param>
    /// <param name="readerNameKeys"></param>
    /// <returns>数据列转换映射实体属性构造器</returns>
    public IDataReaderEntityBuilder<T> CreateBuilder(IDataRecord dataRecord, List<KeyValuePair<string, string>> readerNameKeys)
    {
        var readerKeys = readerNameKeys.Select(s => s.Key).ToList();
        var type = typeof(T);
        //定义动态方法
        var dynamicMethod = new DynamicMethod("DataReaderEntityBuild", type, new[] { typeof(IDataRecord) }, type, true);
        //创建一个IL生成器
        var generator = dynamicMethod.GetILGenerator();
        //声明类型是{type}的局部变量
        var result = generator.DeclareLocal(type);
        //创建一个对象
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
        if (constructor == null)
        {
            throw new EntityFrameworkException(ErrorMessage.EntityMapperBuilderFailed, type.Name);
        }

        generator.Emit(OpCodes.Newobj, constructor);
        //添加到列表中
        generator.Emit(OpCodes.Stloc, result);

        var entityColumns = _entityMapping.GetEntity(type).Columns;
        foreach (var entityColumn in entityColumns)
        {
            var fileName = entityColumn.ColumnName ?? entityColumn.PropertyName;
            if (entityColumn.Ignore && !readerKeys.Any(s => s.Equals(fileName, StringComparison.CurrentCultureIgnoreCase)))
            {
                continue;
            }

            var method = entityColumn.PropertyInfo.GetSetMethod(true);
            if (method == null)
            {
                continue;
            }

            var readerKey = readerKeys.FirstOrDefault(s => s.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
            if (readerKey == null)
            {
                throw new EntityFrameworkException(ErrorMessage.EntityColumnNameNotFound, fileName);
            }

            BindField(dataRecord, generator, result, method, entityColumn, readerKeys.First(s => s.Equals(fileName, StringComparison.CurrentCultureIgnoreCase)));
        }

        generator.Emit(OpCodes.Ldloc, result);
        //方法结束，返回
        generator.Emit(OpCodes.Ret);
        //完成动态方法的创建，并且获取一个可以执行该动态方法的委托
        _handler = (Load)dynamicMethod.CreateDelegate(typeof(Load));
        return this;
    }

    private void BindField(IDataRecord dataRecord, ILGenerator generator, LocalBuilder result, MethodInfo method, EntityColumn entityColumn, string fieldName)
    {
        var i = dataRecord.GetOrdinal(fieldName);
        var endIfLabel = generator.DefineLabel();
        var tryStart = generator.BeginExceptionBlock(); //begin try
        generator.Emit(OpCodes.Ldarg_0);
        generator.Emit(OpCodes.Ldc_I4, i);
        generator.Emit(OpCodes.Callvirt, DataUtils.IsDBNull);
        generator.Emit(OpCodes.Brtrue, endIfLabel);
        generator.Emit(OpCodes.Ldloc, result);
        generator.Emit(OpCodes.Ldarg_0);
        generator.Emit(OpCodes.Ldc_I4, i);
        BindMethod(dataRecord, generator, entityColumn, i);
        generator.Emit(OpCodes.Callvirt, method);
        generator.MarkLabel(endIfLabel);

        generator.Emit(OpCodes.Leave, tryStart); //eng try
        generator.BeginCatchBlock(typeof(Exception)); //begin catch
        generator.Emit(OpCodes.Ldstr, $"{entityColumn.PropertyName}绑定到{entityColumn.PropertyName}失败,可以试着换一个类型，或者使用ORM自定义类型实现"); //throw message
        generator.Emit(OpCodes.Newobj, typeof(Exception).GetConstructor(new[] { typeof(string) }) ?? throw new InvalidOperationException());
        generator.Emit(OpCodes.Throw);
        generator.EndExceptionBlock();
    }

    private void BindMethod(IDataRecord dataRecord, ILGenerator generator, EntityColumn entityColumn, int ordinal)
    {
        var underlyingType = Nullable.GetUnderlyingType(entityColumn.PropertyInfo.PropertyType);
        var isNullableType = underlyingType != null;
        var propertyType = underlyingType ?? entityColumn.PropertyInfo.PropertyType;
        var propertyName = entityColumn.PropertyName;
        var dataTypeName = dataRecord.GetDataTypeName(ordinal);
        MethodInfo method;
        switch (dataTypeName)
        {
            case "BIGINT":
                method = isNullableType ? DataUtils.GetInt64OrNull : DataUtils.GetInt64;
                break;
            case "DATETIME":
            {
                switch (propertyType.Name)
                {
                    case "Datetime":
                        method = isNullableType ? DataUtils.GetDateTimeOrNull : DataUtils.GetDateTime;
                        break;
                    /*case "DateTimeOffset":
                        method = isNullableType ? DataUtils.GetDateTimeOffsetOrNull : DataUtils.GetDateTimeOffset;
                        break;*/
                    default:
                        method = isNullableType ? DataUtils.GetDateTimeOrNull : DataUtils.GetDateTime;
                        break;
                }

                break;
            }


            case "Int16":
                method = isNullableType ? DataUtils.GetInt16OrNull : DataUtils.GetInt16;
                break;
            case "Int32":
                method = isNullableType ? DataUtils.GetInt32OrNull : DataUtils.GetInt32;
                break;
            case "Byte":
                method = isNullableType ? DataUtils.GetByteOrNull : DataUtils.GetByte;
                break;

            case "Boolean":
                method = isNullableType ? DataUtils.GetBooleanOrNull : DataUtils.GetBoolean;
                break;
            case "String":
                method = DataUtils.GetString;
                break;
            case "Datetime":
                method = isNullableType ? DataUtils.GetDateTimeOrNull : DataUtils.GetDateTime;
                break;
            case "DateTimeOffset":
                method = isNullableType ? DataUtils.GetDateTimeOffsetOrNull : DataUtils.GetDateTimeOffset;
                break;
            case "Time":
                method = isNullableType ? DataUtils.GetTimeOrNull : DataUtils.GetTime;
                break;
            case "Decimal":
                method = isNullableType ? DataUtils.GetDecimalOrNull : DataUtils.GetDecimal;
                break;
            case "Float":
                method = isNullableType ? DataUtils.GetFloatOrNull : DataUtils.GetFloat;
                break;
            case "Double":
                method = isNullableType ? DataUtils.GetDoubleOrNull : DataUtils.GetDouble;
                break;
            case "Guid":
                method = isNullableType ? DataUtils.GetGuidOrNull : DataUtils.GetGuid;
                break;
            case "Enum":
                method = isNullableType ? DataUtils.GetEnumOrNull.MakeGenericMethod(propertyType) : DataUtils.GetEnum.MakeGenericMethod(propertyType);
                break;
            default:
                throw new EntityFrameworkException($"{propertyName} can't  convert {dataTypeName} to {propertyName}");
        }

        generator.Emit(method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, method);
    }

    private delegate T Load(IDataRecord dataRecord);
}