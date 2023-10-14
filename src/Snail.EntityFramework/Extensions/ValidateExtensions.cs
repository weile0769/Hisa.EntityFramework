namespace System.Collections.Generic;

internal static class ValidateExtensions
{
    internal static bool IsIn<T>(this T thisValue, params T[] values)
    {
        return values.Contains(thisValue);
    }
}