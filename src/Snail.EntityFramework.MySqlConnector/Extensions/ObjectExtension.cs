namespace Snail.EntityFramework.MySqlConnector.Extensions;

/// <summary>
///     Object扩展类
/// </summary>
internal static class ObjectExtension
{
    /// <summary>
    ///     Object对象转字符串
    /// </summary>
    /// <remarks>
    ///     传入对象为空，则取默认值：空字符串
    /// </remarks>
    /// <param name="value"></param>
    /// <param name="defaultString"></param>
    /// <returns></returns>
    internal static string ToSafeString(this object value, string defaultString = "")
    {
        return value != null ? value.ToString()?.Trim() : defaultString;
    }
}