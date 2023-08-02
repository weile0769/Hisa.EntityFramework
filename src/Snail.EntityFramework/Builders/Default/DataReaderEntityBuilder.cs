using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using Snail.EntityFramework.Exceptions;
using Snail.EntityFramework.Models;
using Snail.EntityFramework.Utils;

namespace Snail.EntityFramework.Builders.Default;

public class DataReaderEntityBuilder<T> : IDataReaderEntityBuilder<T>
{
    private static readonly MethodInfo GetByte = typeof(IDataRecord).GetMethod("GetByte", new[] { typeof(int) });
    private static readonly MethodInfo GetInt16 = typeof(IDataRecord).GetMethod("GetInt16", new[] { typeof(int) });
    private static readonly MethodInfo GetInt32 = typeof(IDataRecord).GetMethod("GetInt32", new[] { typeof(int) });
    private static readonly MethodInfo GetInt64 = typeof(IDataRecord).GetMethod("GetInt64", new[] { typeof(int) });
    private static readonly MethodInfo GetBoolean = typeof(IDataRecord).GetMethod("GetBoolean", new[] { typeof(int) });
    private static readonly MethodInfo GetString = typeof(IDataRecord).GetMethod("GetString", new[] { typeof(int) });
    private static readonly MethodInfo GetDateTime = typeof(IDataRecord).GetMethod("GetDateTime", new Type[] { typeof(int) });
    
    private static readonly MethodInfo GetByteOrNull = typeof(IDataRecordExtensions).GetMethod("GetByteOrNull");
    private static readonly MethodInfo GetInt16OrNull = typeof(IDataRecordExtensions).GetMethod("GetInt16OrNull");
    private static readonly MethodInfo GetInt32OrNull = typeof(IDataRecordExtensions).GetMethod("GetInt32OrNull");
    private static readonly MethodInfo GetInt64OrNull = typeof(IDataRecordExtensions).GetMethod("GetInt64OrNull");
    private static readonly MethodInfo GetBooleanOrNull = typeof(IDataRecordExtensions).GetMethod("GetBooleanOrNull");
    private static readonly MethodInfo GetDateTimeOrNull = typeof(IDataRecordExtensions).GetMethod("GetDateTimeOrNull");
    private static readonly MethodInfo GetTimeOrNull = typeof(IDataRecordExtensions).GetMethod("GetTimeOrNull");
    private static readonly MethodInfo GetTime = typeof(IDataRecordExtensions).GetMethod("GetTime");
    
    private readonly IEntityMappingProvider _entityMapping;
    private IDataRecord _dataRecord;
    private List<string> _readerKeys;
    private DataReaderEntityBuilder<T> DynamicBuilder;
    private Load handler;

    public DataReaderEntityBuilder(IEntityMappingProvider entityMapping)
    {
        _entityMapping = entityMapping;
    }

    public void InitEntityBuilder(IDataRecord dataRecord, List<string> readerKeys)
    {
        _dataRecord = dataRecord;
        _readerKeys = readerKeys;
    }


    public IDataReaderEntityBuilder<T> CreateEntityBuilder(Type type)
    {
        if (type == null)
        {
            return DynamicBuilder;
        }

        //定义动态方法，方法名：EntityBuilder，返回值
        var dynamicMethod = new DynamicMethod("EntityBuild", type, new[] { typeof(IDataRecord) }, type, true);
        //创建一个IL生成器，为动态方法生成代码
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
            if (entityColumn.Ignore && !_readerKeys.Any(s => s.Equals(fileName, StringComparison.CurrentCultureIgnoreCase)))
            {
                continue;
            }

            var method = entityColumn.PropertyInfo.GetSetMethod(true);
            if (method == null)
            {
                continue;
            }

            BindField(generator, result, method, entityColumn, _readerKeys.First(s => s.Equals(fileName, StringComparison.CurrentCultureIgnoreCase)));
        }

        generator.Emit(OpCodes.Ldloc, result);
        //方法结束，返回
        generator.Emit(OpCodes.Ret);
        //完成动态方法的创建，并且获取一个可以执行该动态方法的委托
        DynamicBuilder.handler = (Load)dynamicMethod.CreateDelegate(typeof(Load));
        return DynamicBuilder;
    }

    private void BindField(ILGenerator generator, LocalBuilder result, MethodInfo method, EntityColumn entityColumn, string fieldName)
    {
        var i = _dataRecord.GetOrdinal(fieldName);
        var endIfLabel = generator.DefineLabel();

        //2023-3-8
        var tryStart = generator.BeginExceptionBlock(); //begin try
        //2023-3-8 

        generator.Emit(OpCodes.Ldarg_0);
        generator.Emit(OpCodes.Ldc_I4, i);
        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
        generator.Emit(OpCodes.Brtrue, endIfLabel);
        generator.Emit(OpCodes.Ldloc, result);
        generator.Emit(OpCodes.Ldarg_0);
        generator.Emit(OpCodes.Ldc_I4, i);
        BindMethod(generator, entityColumn, i);
        generator.Emit(OpCodes.Callvirt, method);
        generator.MarkLabel(endIfLabel);

        //2023-3-8
        generator.Emit(OpCodes.Leave, tryStart); //eng try
        generator.BeginCatchBlock(typeof(Exception)); //begin catch
        generator.Emit(OpCodes.Ldstr, ErrorMessage.GetThrowMessage($"{entityColumn.EntityName} {entityColumn.PropertyName} bind error", $"{entityColumn.PropertyName}绑定到{entityColumn.EntityName}失败,可以试着换一个类型，或者使用ORM自定义类型实现")); //thow message
        generator.Emit(OpCodes.Newobj, typeof(Exception).GetConstructor(new[] { typeof(string) }));
        generator.Emit(OpCodes.Throw);
        generator.EndExceptionBlock();
        //2023-3-8
    }

    private void BindMethod(ILGenerator generator, EntityColumn entityColumn, int ordinal)
    {
        var underlyingType = Nullable.GetUnderlyingType(entityColumn.PropertyInfo.PropertyType);
        var isNullableType = underlyingType != null;
        var propertyType = underlyingType ?? entityColumn.PropertyInfo.PropertyType;
        var dataTypeName = _dataRecord.GetDataTypeName(ordinal);
        if (string.IsNullOrWhiteSpace(dataTypeName))
        {
            dataTypeName = propertyType.Name;
        }

        MethodInfo method = null;
        switch (dataTypeName)
        {
            case "Int16":
                method = isNullableType ? GetInt16OrNull : GetInt16;
                break;
            case "Int32":
                method = isNullableType ? GetInt32OrNull : GetInt32;
                break;
            case "Int64":
                method = isNullableType ? GetInt64OrNull : GetInt64;
                break;
            case "Byte":
                method = isNullableType ? GetByteOrNull : GetByte;
                break;
            case "Boolean":
                method = isNullableType ? GetBooleanOrNull : GetBoolean;
                break;
            case "String":
                method = GetString;
                break;
            case "Datetime":
                method = isNullableType ? GetDateTimeOrNull : GetDateTime;
                break;
            case "Time":
                method = isNullableType ? GetTimeOrNull : GetTime;
                break; 


            case CSharpDataType.@decimal:
                CheckType(bind.DecimalThrow, bindProperyTypeName, validPropertyName, propertyName);
                if (bindProperyTypeName == "decimal")
                {
                    method = isNullableType ? getConvertDecimal : getDecimal;
                }

                break;
            case CSharpDataType.@float:
            case CSharpDataType.@double:
                CheckType(bind.DoubleThrow, bindProperyTypeName, validPropertyName, propertyName);
                if (bindProperyTypeName.IsIn("double", "single") && dbTypeName != "real")
                {
                    method = isNullableType ? getConvertDouble : getDouble;
                }
                else
                {
                    method = isNullableType ? getConvertFloat : getFloat;
                }

                if (dbTypeName.Equals("float", StringComparison.CurrentCultureIgnoreCase) && isNullableType && bindProperyTypeName.Equals("single", StringComparison.CurrentCultureIgnoreCase))
                {
                    method = getConvertDoubleToFloat;
                }

                if (bindPropertyType == UtilConstants.DecType)
                {
                    method = isNullableType ? getOtherNull.MakeGenericMethod(bindPropertyType) : getOther.MakeGenericMethod(bindPropertyType);
                }

                if (bindPropertyType == UtilConstants.IntType)
                {
                    method = isNullableType ? getOtherNull.MakeGenericMethod(bindPropertyType) : getOther.MakeGenericMethod(bindPropertyType);
                }

                break;
            case CSharpDataType.Guid:
                CheckType(bind.GuidThrow, bindProperyTypeName, validPropertyName, propertyName);
                if (bindProperyTypeName == "guid")
                {
                    method = isNullableType ? getConvertGuid : getGuid;
                }

                break;
            case CSharpDataType.@byte:
                if (bindProperyTypeName == "byte")
                {
                    method = isNullableType ? getConvertByte : GetByte;
                }

                break;
            case CSharpDataType.@enum:
                method = isNullableType ? getConvertEnum_Null.MakeGenericMethod(bindPropertyType) : getEnum.MakeGenericMethod(bindPropertyType);
                break;
            case CSharpDataType.@short:
                CheckType(bind.ShortThrow, bindProperyTypeName, validPropertyName, propertyName);
                if (bindProperyTypeName == "int16" || bindProperyTypeName == "short")
                {
                    method = isNullableType ? getConvertInt16 : GetInt16;
                }

                break;
            case CSharpDataType.@long:
                if (bindProperyTypeName == "int64" || bindProperyTypeName == "long")
                {
                    method = isNullableType ? getConvertInt64 : GetInt64;
                }

                break;
            case CSharpDataType.DateTimeOffset:
                method = isNullableType ? getConvertdatetimeoffset : getdatetimeoffset;
                if (bindProperyTypeName == "datetime")
                {
                    method = isNullableType ? getConvertdatetimeoffsetDate : getdatetimeoffsetDate;
                }

                break;
            case CSharpDataType.Single:
                break;
            default:
                method = isNullableType ? getOtherNull.MakeGenericMethod(bindPropertyType) : getOther.MakeGenericMethod(bindPropertyType);
                break;
        }

        if (method == null && bindPropertyType == UtilConstants.StringType)
        {
            method = getConvertString;
        }

        if (bindPropertyType == UtilConstants.ObjType)
        {
            method = isNullableType ? getOtherNull.MakeGenericMethod(bindPropertyType) : getOther.MakeGenericMethod(bindPropertyType);
        }

        if (method == null)
        {
            method = isNullableType ? getOtherNull.MakeGenericMethod(bindPropertyType) : getOther.MakeGenericMethod(bindPropertyType);
        }


        if (method.IsVirtual)
        {
            generator.Emit(OpCodes.Callvirt, method);
        }
        else
        {
            generator.Emit(OpCodes.Call, method);
        }
    }

    private delegate T Load(IDataRecord dataRecord);
}