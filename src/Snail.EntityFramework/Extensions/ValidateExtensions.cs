namespace System.Collections.Generic;

internal static class ValidateExtensions
{
    internal static bool HasValue(this IEnumerable<object> thisValue)
    {
        return thisValue != null && thisValue.Any();
    }

    internal static bool IsIn<T>(this T thisValue, params T[] values)
    {
        return values.Contains(thisValue);
    }
}