namespace Lysa.EntityFramework.MySqlConnector.Extensions;

/// <summary>
///     Validate 验证扩展类
/// </summary>
internal static class ValidateExtension
{
    /// <summary>
    ///     是否集合列表
    /// </summary>
    /// <param name="thisValue">字符串</param>
    /// <returns>ture：是；false：否</returns>
    internal static bool IsCollections(this string thisValue)
    {
        return (thisValue + "").StartsWith("System.Collections.Generic.List") || (thisValue + "").StartsWith("System.Collections.Generic.IEnumerable");
    }

    /// <summary>
    ///     元素T是否在数组T[]里
    /// </summary>
    /// <param name="thisValue">值</param>
    /// <param name="values">数组</param>
    /// <typeparam name="T">元素类型</typeparam>
    /// <returns></returns>
    internal static bool IsIn<T>(this T thisValue, params T[] values)
    {
        return values.Contains(thisValue);
    }

    /// <summary>
    ///     是否数值类型数组
    /// </summary>
    /// <param name="type">数值类型</param>
    /// <returns>ture：是 false:否</returns>
    internal static bool IsNumberArray(this Type type)
    {
        return type.IsIn(typeof(int[]),
            typeof(long[]),
            typeof(short[]),
            typeof(uint[]),
            typeof(ulong[]),
            typeof(ushort[]),
            typeof(int?[]),
            typeof(long?[]),
            typeof(short?[]),
            typeof(uint?[]),
            typeof(ulong?[]),
            typeof(ushort?[]),
            typeof(List<int>),
            typeof(List<long>),
            typeof(List<short>),
            typeof(List<uint>),
            typeof(List<ulong>),
            typeof(List<ushort>),
            typeof(List<int?>),
            typeof(List<long?>),
            typeof(List<short?>),
            typeof(List<uint?>),
            typeof(List<ulong?>),
            typeof(List<ushort?>));
    }

    /// <summary>
    ///     判断值是否为空
    /// </summary>
    /// <param name="thisValue">值</param>
    /// <returns>ture：是 false:否</returns>
    internal static bool IsNullOrEmpty(this object thisValue)
    {
        if (thisValue == null || thisValue == DBNull.Value)
        {
            return true;
        }

        return thisValue.ToString() == string.Empty;
    }
}