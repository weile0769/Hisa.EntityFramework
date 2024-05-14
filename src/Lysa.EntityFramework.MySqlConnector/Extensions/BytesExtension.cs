namespace System.Text;

public static class BytesExtension
{
    public static string BytesSqlRaw(this byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var vc in bytes)
        {
            sb.Append(vc.ToString("X2"));
        }

        return sb.ToString();
    }
}