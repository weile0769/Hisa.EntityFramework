namespace System.Collections.Generic;

/// <summary>
///     校验扩展函数
/// </summary>
internal static class ValidateExtension
{
    /// <summary>
    ///     是否有值
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    internal static bool HasValue(this IEnumerable<object> thisValue)
    {
        return thisValue != null && thisValue.Any();
    }

    /// <summary>
    ///     是否有值
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    internal static bool HasValue(this object thisValue)
    {
        if (thisValue == null || thisValue == DBNull.Value)
        {
            return false;
        }

        return thisValue.ToString() != "";
    }

    /// <summary>
    ///     当前元素是否在数组内
    /// </summary>
    /// <param name="thisValue">当前元素</param>
    /// <param name="values">数组</param>
    /// <typeparam name="T">判断类型</typeparam>
    /// <returns>true：是 false：否</returns>
    internal static bool IsIn<T>(this T thisValue, params T[] values)
    {
        return values.Contains(thisValue);
    }
}