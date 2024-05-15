using System.Collections;
using System.Text;
using Hisa.EntityFramework.Providers;

namespace Lysa.EntityFramework.MySqlConnector.Providers;

public class MySqlParameterFormatValueProvider : ISqlParameterFormatValueProvider
{
    public object FormatSqlValue(object parameterValue)
    {
        switch (parameterValue)
        {
            case null:
                return "NULL";
            case bool b:
                return b ? 1 : 0;
            case uint:
            case int:
            case ushort:
            case short:
            case ulong:
            case long:
            case float:
            case double:
            case decimal:
                return parameterValue;
            case string:
                return string.Concat("'", parameterValue.ToString()?.Replace("'", "''").Replace("\\", @"\\"), "'");
            case char:
                return string.Concat("'", parameterValue.ToString()?.Replace("'", "''").Replace("\\", @"\\").Replace('\0', ' '), "'");
            case Enum:
                return string.Concat("'", parameterValue.ToString()?.Replace("'", "''").Replace("\\", @"\\").Replace(", ", ","), "'");
            case DateTime time:
                return string.Concat("'", time.ToString("yyyy-MM-dd HH:mm:ss.fff"), "'");
            case TimeSpan span:
                return span.Ticks / 10;
            case byte[] bytes:
                return $"0x{bytes.BytesSqlRaw()}";
            case IEnumerable:
                return FormatParameterValueFormIEnumerable(parameterValue);
            default:
                return string.Concat("'", parameterValue.ToString()?.Replace("'", "''").Replace("\\", @"\\"), "'");
        }
    }

    private string FormatParameterValueFormIEnumerable(object param)
    {
        var sb = new StringBuilder();
        var ie = param as IEnumerable;
        var idx = 0;
        foreach (var z in ie)
        {
            sb.Append(',');
            if (++idx > 500)
            {
                sb.Append("   \r\n    \r\n"); //500元素分割, 3空格\r\n4空格
                idx = 1;
            }

            sb.Append(FormatSqlValue(z));
        }

        return sb.Length == 0 ? "(NULL)" : sb.Remove(0, 1).Insert(0, "(").Append(')').ToString();
    }
}