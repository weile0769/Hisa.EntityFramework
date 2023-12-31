using System.Text;

namespace System.Collections.Generic;

internal static class IListExtension
{
    internal static string ForeachSelectString(this IEnumerable<string> listItems)
    {
        var builder = new StringBuilder();
        foreach (var listItem in listItems)
        {
            builder.Append(listItem);
        }

        return builder.ToString();
    }
}