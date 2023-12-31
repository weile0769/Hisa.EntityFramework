namespace System.Data;

public static class IDataRecordExtension
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

    public static decimal? GetDecimalOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetDecimal(i);
        return result;
    }

    public static float? GetFloatOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetFloat(i);
        return result;
    }

    public static double? GetDoubleOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var result = dr.GetDouble(i);
        return result;
    }

    public static Guid? GetGuidOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return Guid.Empty;
        }

        var result = Guid.Parse(dr.GetValue(i).ToString());
        return result;
    }

    public static T? GetEnumOrNull<T>(this IDataReader dr, int i) where T : struct
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var value = dr.GetValue(i);
        if (value != null)
        {
            var valueType = value.GetType();
            if (valueType == typeof(float) || valueType == typeof(decimal) || valueType == typeof(double))
            {
                if (Convert.ToDecimal(value) < 0)
                {
                    value = Convert.ToInt32(value);
                }
                else
                {
                    value = Convert.ToUInt32(value);
                }
            }
            else if (valueType == typeof(string))
            {
                return (T)Enum.Parse(typeof(T), value.ToString().Trim());
            }
        }

        var t = (T)Enum.ToObject(typeof(T), value);
        return t;
    }

    public static T GetEnum<T>(this IDataReader dr, int i) where T : struct
    {
        var value = dr.GetValue(i);
        if (value != null)
        {
            var valueType = value.GetType();
            if (valueType == typeof(float) || valueType == typeof(decimal) || valueType == typeof(double))
            {
                if (Convert.ToDecimal(value) < 0)
                {
                    value = Convert.ToInt32(value);
                }
                else
                {
                    value = Convert.ToUInt32(value);
                }
            }
            else if (valueType == typeof(string))
            {
                return (T)Enum.Parse(typeof(T), value.ToString().Trim());
            }
        }

        var t = (T)Enum.ToObject(typeof(T), value);
        return t;
    }

    public static DateTime? GetDateTimeOffsetOrNull(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return null;
        }

        var value = dr.GetDateTime(i);
        if (value == DateTime.MinValue)
        {
            return null;
        }

        var offsetValue = (DateTimeOffset)dr.GetValue(i);
        var result = offsetValue.DateTime;
        return result;
    }

    public static DateTimeOffset GetDateTimeOffset(this IDataRecord dr, int i)
    {
        if (dr.IsDBNull(i))
        {
            return default;
        }

        var date = dr.GetValue(i);
        if (date is DateTime time)
        {
            return new DateTimeOffset(time);
        }

        var result = (DateTimeOffset)date;
        return result;
    }
}