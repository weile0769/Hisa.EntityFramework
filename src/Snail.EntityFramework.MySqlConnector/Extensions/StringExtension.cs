namespace System;

/// <summary>
///     String扩展类
/// </summary>
internal static class StringExtension
{
    /// <summary>
    ///     转化裁减多余空格后的字符串
    /// </summary>
    /// <remarks>
    ///     安全：
    /// </remarks>
    /// <param name="value"></param>
    /// <param name="defaultString"></param>
    /// <returns></returns>
    internal static string ToSafeString(this string value, string defaultString = "")
    {
        return value?.Trim() ?? defaultString;
    }
}