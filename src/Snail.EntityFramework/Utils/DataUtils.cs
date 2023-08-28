using System.Data;
using System.Reflection;

namespace Snail.EntityFramework.Utils;

/// <summary>
///     IDataRecord工具类
/// </summary>
internal static class DataUtils
{
    #region IDataRecord 扩展反射函数

    internal static readonly MethodInfo IsDbNull = typeof(IDataRecord).GetMethod("IsDBNull", new[] { typeof(int) });
    internal static readonly MethodInfo GetByte = typeof(IDataRecord).GetMethod("GetByte", new[] { typeof(int) });
    internal static readonly MethodInfo GetInt16 = typeof(IDataRecord).GetMethod("GetInt16", new[] { typeof(int) });
    internal static readonly MethodInfo GetInt32 = typeof(IDataRecord).GetMethod("GetInt32", new[] { typeof(int) });
    internal static readonly MethodInfo GetInt64 = typeof(IDataRecord).GetMethod("GetInt64", new[] { typeof(int) });
    internal static readonly MethodInfo GetBoolean = typeof(IDataRecord).GetMethod("GetBoolean", new[] { typeof(int) });
    internal static readonly MethodInfo GetString = typeof(IDataRecord).GetMethod("GetString", new[] { typeof(int) });
    internal static readonly MethodInfo GetDateTime = typeof(IDataRecord).GetMethod("GetDateTime", new[] { typeof(int) });
    internal static readonly MethodInfo GetDecimal = typeof(IDataRecord).GetMethod("GetDecimal", new[] { typeof(int) });
    internal static readonly MethodInfo GetFloat = typeof(IDataRecord).GetMethod("GetFloat", new[] { typeof(int) });
    internal static readonly MethodInfo GetDouble = typeof(IDataRecord).GetMethod("GetDouble", new[] { typeof(int) });
    internal static readonly MethodInfo GetGuid = typeof(IDataRecord).GetMethod("GetGuid", new[] { typeof(int) });

    internal static readonly MethodInfo GetByteOrNull = typeof(IDataRecordExtensions).GetMethod("GetByteOrNull");
    internal static readonly MethodInfo GetInt16OrNull = typeof(IDataRecordExtensions).GetMethod("GetInt16OrNull");
    internal static readonly MethodInfo GetInt32OrNull = typeof(IDataRecordExtensions).GetMethod("GetInt32OrNull");
    internal static readonly MethodInfo GetInt64OrNull = typeof(IDataRecordExtensions).GetMethod("GetInt64OrNull");
    internal static readonly MethodInfo GetBooleanOrNull = typeof(IDataRecordExtensions).GetMethod("GetBooleanOrNull");
    internal static readonly MethodInfo GetDateTimeOrNull = typeof(IDataRecordExtensions).GetMethod("GetDateTimeOrNull");
    internal static readonly MethodInfo GetDateTimeOffset = typeof(IDataRecordExtensions).GetMethod("GetDateTimeOffset");
    internal static readonly MethodInfo GetDateTimeOffsetOrNull = typeof(IDataRecordExtensions).GetMethod("GetDateTimeOffsetOrNull");
    internal static readonly MethodInfo GetTimeOrNull = typeof(IDataRecordExtensions).GetMethod("GetTimeOrNull");
    internal static readonly MethodInfo GetTime = typeof(IDataRecordExtensions).GetMethod("GetTime");
    internal static readonly MethodInfo GetDecimalOrNull = typeof(IDataRecordExtensions).GetMethod("GetDecimalOrNull");
    internal static readonly MethodInfo GetFloatOrNull = typeof(IDataRecordExtensions).GetMethod("GetFloatOrNull");
    internal static readonly MethodInfo GetDoubleOrNull = typeof(IDataRecordExtensions).GetMethod("GetDoubleOrNull");
    internal static readonly MethodInfo GetGuidOrNull = typeof(IDataRecordExtensions).GetMethod("GetGuidOrNull");
    internal static readonly MethodInfo GetEnumOrNull = typeof(IDataRecordExtensions).GetMethod("GetEnumOrNull");
    internal static readonly MethodInfo GetEnum = typeof(IDataRecordExtensions).GetMethod("GetEnum");

    #endregion
}