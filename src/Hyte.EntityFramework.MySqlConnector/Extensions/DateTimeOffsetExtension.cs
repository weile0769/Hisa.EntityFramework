namespace System;

/// <summary>
///     DateTimeOffset扩展类
/// </summary>
internal static class DateTimeOffsetExtension
{
    /// <summary>
    ///     DataTimeOffset转DateTime
    /// </summary>
    /// <param name="dateTime">DateTimeOffset时间对象</param>
    /// <returns>DateTime时间对象</returns>
    internal static DateTime ConvertToDateTime(this DateTimeOffset dateTime)
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