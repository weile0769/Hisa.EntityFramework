namespace System.Data;

public static class IDataRecordExtensions
{
    public static DateTime? GetDateTimeOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetDateTime(i);
        if (result == DateTime.MinValue)
        {
            return null;
        }

        return result;
    }

    public static bool? GetBooleanOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetBoolean(i);
        return result;
    }

    public static short? GetInt16OrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetInt16(i);
        return result;
    }

    public static int? GetInt32OrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetInt32(i);
        return result;
    }

    public static long? GetInt64OrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetInt64(i);
        return result;
    }

    public static byte? GetByteOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetByte(i);
        return result;
    }

    public static DateTime? GetTimeOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetValue(i);
        if (result == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDateTime(result.ToString());
    }

    public static DateTime GetTime(this IDataRecord dr, int i)
    {
        return Convert.ToDateTime(dr.GetValue(i).ToString());
    }
}