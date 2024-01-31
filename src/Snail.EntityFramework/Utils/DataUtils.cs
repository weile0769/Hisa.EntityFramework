using System.Data;
using System.Reflection;

namespace Snail.EntityFramework.Utils;

/// <summary>
///     IDataRecord工具类
/// </summary>
internal static class DataUtils
{
    #region IDataRecord 扩展反射函数

    internal static readonly MethodInfo IsDBNull = typeof(IDataRecord).GetMethod("IsDBNull", new[] { typeof(int) });
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

    internal static readonly MethodInfo GetByteOrNull = typeof(IDataRecordExtension).GetMethod("GetByteOrNull");
    internal static readonly MethodInfo GetInt16OrNull = typeof(IDataRecordExtension).GetMethod("GetInt16OrNull");
    internal static readonly MethodInfo GetInt32OrNull = typeof(IDataRecordExtension).GetMethod("GetInt32OrNull");
    internal static readonly MethodInfo GetInt64OrNull = typeof(IDataRecordExtension).GetMethod("GetInt64OrNull");
    internal static readonly MethodInfo GetBooleanOrNull = typeof(IDataRecordExtension).GetMethod("GetBooleanOrNull");
    internal static readonly MethodInfo GetDateTimeOrNull = typeof(IDataRecordExtension).GetMethod("GetDateTimeOrNull");
    internal static readonly MethodInfo GetDateTimeOffset = typeof(IDataRecordExtension).GetMethod("GetDateTimeOffset");
    internal static readonly MethodInfo GetDateTimeOffsetOrNull = typeof(IDataRecordExtension).GetMethod("GetDateTimeOffsetOrNull");
    internal static readonly MethodInfo GetTimeOrNull = typeof(IDataRecordExtension).GetMethod("GetTimeOrNull");
    internal static readonly MethodInfo GetTime = typeof(IDataRecordExtension).GetMethod("GetTime");
    internal static readonly MethodInfo GetDecimalOrNull = typeof(IDataRecordExtension).GetMethod("GetDecimalOrNull");
    internal static readonly MethodInfo GetFloatOrNull = typeof(IDataRecordExtension).GetMethod("GetFloatOrNull");
    internal static readonly MethodInfo GetDoubleOrNull = typeof(IDataRecordExtension).GetMethod("GetDoubleOrNull");
    internal static readonly MethodInfo GetGuidOrNull = typeof(IDataRecordExtension).GetMethod("GetGuidOrNull");
    internal static readonly MethodInfo GetEnumOrNull = typeof(IDataRecordExtension).GetMethod("GetEnumOrNull");
    internal static readonly MethodInfo GetEnum = typeof(IDataRecordExtension).GetMethod("GetEnum");

    #endregion
}