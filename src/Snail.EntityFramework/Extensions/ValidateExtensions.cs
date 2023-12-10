namespace System.Collections.Generic;

internal static class ValidateExtensions
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
    public static bool HasValue(this object thisValue)
    {
        if (thisValue == null || thisValue == DBNull.Value)
        {
            return false;
        }

        return thisValue.ToString() != "";
    }


    internal static bool IsIn<T>(this T thisValue, params T[] values)
    {
        return values.Contains(thisValue);
    }
}