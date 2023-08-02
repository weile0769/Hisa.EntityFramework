namespace Snail.EntityFramework.MySqlConnector.Utils;

/// <summary>
///     公共帮助函数
/// </summary>
internal static class UtilMethods
{
    /// <summary>
    ///     DateTimeOffset转DateTime
    /// </summary>
    /// <param name="dateTime">时间类型</param>
    /// <returns></returns>
    internal static DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
    {
        if (dateTime.Offset.Equals(TimeSpan.Zero))
        {
            return dateTime.UtcDateTime;
        }

        if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
        {
            return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
        }

        return dateTime.DateTime;
    }
}