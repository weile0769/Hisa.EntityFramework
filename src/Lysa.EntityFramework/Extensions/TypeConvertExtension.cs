namespace System;

/// <summary>
///     类型转换扩展
/// </summary>
internal static class TypeConvertExtension
{
    /// <summary>
    ///     是否数字类型
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>true：是 false：否</returns>
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
}