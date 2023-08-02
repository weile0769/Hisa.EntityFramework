namespace System.Collections.Generic;

/// <summary>
///     IEnumerable扩展类
/// </summary>
internal static class EnumerableExtension
{
    /// <summary>
    ///     集合是否存在元素
    /// </summary>
    /// <param name="values">元素集合</param>
    /// <returns>true：存在值  false：不存在元素</returns>
    internal static bool HasValue(this IEnumerable<object> values)
    {
        return values != null && values.Any();
    }
}