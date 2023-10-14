using System.Data;
using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.MySqlConnector.XUnit.UnitTests;

/// <summary>
///     SqlParameterTypeConvertProvider单元测试
/// </summary>
public class SqlParameterTypeConvertProviderUnitTest
{
    /// <summary>
    ///     数据参数化类型转换提供器
    /// </summary>
    private readonly ISqlParameterTypeConvertProvider _convertProvider;


    /// <summary>
    ///     构造函数
    /// </summary>
    public SqlParameterTypeConvertProviderUnitTest(ISqlParameterTypeConvertProvider convertProvider)
    {
        _convertProvider = convertProvider;
    }

    /// <summary>
    ///     ushort转UInt16单元测试案例
    /// </summary>
    [Fact(DisplayName = "ushort转UInt16单元测试案例")]
    public void UInt16ConvertDataTypeUnitTest()
    {
        var propertyType = typeof(ushort);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.UInt16, dataType);
    }

    /// <summary>
    ///     short转Int16单元测试案例
    /// </summary>
    [Fact(DisplayName = "short转Int16单元测试案例")]
    public void Int16ConvertDataTypeUnitTest()
    {
        var propertyType = typeof(short);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Int16, dataType);
    }

    /// <summary>
    ///     uint转UInt32单元测试案例
    /// </summary>
    [Fact(DisplayName = "uint转UInt32单元测试案例")]
    public void UInt32ConvertDataTypeUnitTest()
    {
        var propertyType = typeof(uint);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.UInt32, dataType);
    }

    /// <summary>
    ///     int转UInt32单元测试案例
    /// </summary>
    [Fact(DisplayName = "int转Int32单元测试案例")]
    public void Int32ConvertDataTypeUnitTest()
    {
        var propertyType = typeof(int);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Int32, dataType);
    }

    /// <summary>
    ///     ulong转UInt64单元测试案例
    /// </summary>
    [Fact(DisplayName = "ulong转UInt64单元测试案例")]
    public void UInt64ConvertDataTypeUnitTest()
    {
        var propertyType = typeof(ulong);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.UInt64, dataType);
    }

    /// <summary>
    ///     long转Int64单元测试案例
    /// </summary>
    [Fact(DisplayName = "long转Int64单元测试案例")]
    public void Int64ConvertDataTypeUnitTest()
    {
        var propertyType = typeof(long);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Int64, dataType);
    }

    /// <summary>
    ///     byte转UInt64单元测试案例
    /// </summary>
    [Fact(DisplayName = "byte转Byte单元测试案例")]
    public void ByteConvertDataTypeUnitTest()
    {
        var propertyType = typeof(byte);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Byte, dataType);
    }

    /// <summary>
    ///     sbyte转SByte单元测试案例
    /// </summary>
    [Fact(DisplayName = "sbyte转SByte单元测试案例")]
    public void SByteConvertDataTypeUnitTest()
    {
        var propertyType = typeof(sbyte);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.SByte, dataType);
    }

    /// <summary>
    ///     bool转Boolean单元测试案例
    /// </summary>
    [Fact(DisplayName = "bool转Boolean单元测试案例")]
    public void BoolConvertDataTypeUnitTest()
    {
        var propertyType = typeof(bool);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Boolean, dataType);
    }

    /// <summary>
    ///     Guid转Guid单元测试案例
    /// </summary>
    [Fact(DisplayName = "Guid转Guid单元测试案例")]
    public void GuidConvertDataTypeUnitTest()
    {
        var propertyType = typeof(Guid);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Guid, dataType);
    }

    /// <summary>
    ///     double转Double单元测试案例
    /// </summary>
    [Fact(DisplayName = "double转Double单元测试案例")]
    public void DoubleConvertDataTypeUnitTest()
    {
        var propertyType = typeof(double);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Double, dataType);
    }

    /// <summary>
    ///     float转Single单元测试案例
    /// </summary>
    [Fact(DisplayName = "float转Single单元测试案例")]
    public void FloatConvertDataTypeUnitTest()
    {
        var propertyType = typeof(float);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Single, dataType);
    }

    /// <summary>
    ///     decimal转Decimal单元测试案例
    /// </summary>
    [Fact(DisplayName = "decimal转Decimal单元测试案例")]
    public void DecimalConvertDataTypeUnitTest()
    {
        var propertyType = typeof(decimal);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Decimal, dataType);
    }

    /// <summary>
    ///     string转String单元测试案例
    /// </summary>
    [Fact(DisplayName = "string转String单元测试案例")]
    public void StringConvertDataTypeUnitTest()
    {
        var propertyType = typeof(string);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.String, dataType);
    }

    /// <summary>
    ///     DateTime转DateTime单元测试案例
    /// </summary>
    [Fact(DisplayName = "DateTime转DateTime单元测试案例")]
    public void DateTimeConvertDataTypeUnitTest()
    {
        var propertyType = typeof(DateTime);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.DateTime, dataType);
    }

    /// <summary>
    ///     DateTimeOffset转DateTimeOffset单元测试案例
    /// </summary>
    [Fact(DisplayName = "DateTimeOffset转DateTimeOffset单元测试案例")]
    public void DateTimeOffsetConvertDataTypeUnitTest()
    {
        var propertyType = typeof(DateTimeOffset);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.DateTimeOffset, dataType);
    }

    /// <summary>
    ///     TimeSpan转Time单元测试案例
    /// </summary>
    [Fact(DisplayName = "TimeSpan转Time单元测试案例")]
    public void TimeSpanConvertDataTypeUnitTest()
    {
        var propertyType = typeof(TimeSpan);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Time, dataType);
    }

    /// <summary>
    ///     byte[]转Binary单元测试案例
    /// </summary>
    [Fact(DisplayName = "byte[]转Binary单元测试案例")]
    public void ByteArrayConvertDataTypeUnitTest()
    {
        var propertyType = typeof(byte[]);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Binary, dataType);
    }

    /// <summary>
    ///     Enum转Int64单元测试案例
    /// </summary>
    [Fact(DisplayName = "Enum转Int64单元测试案例")]
    public void EnumConvertDataTypeUnitTest()
    {
        var propertyType = typeof(TypeConvertEnum);
        var dataType = _convertProvider.ConvertDataType(propertyType);
        Assert.Equal(DbType.Int64, dataType);
    }

    /// <summary>
    ///     测试枚举
    /// </summary>
    private enum TypeConvertEnum
    {
        Convert = 1,
        Type = 2
    }
}