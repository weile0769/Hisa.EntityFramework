namespace System;

internal static class TypeConvertExtension
{
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