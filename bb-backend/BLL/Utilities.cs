using System.Security.Cryptography;
using System.Text;

namespace BLL;

public static class Utilities
{
    public static string Md5(this string stringToHash)
    {
        using var sha1 = MD5.Create();
        return string.Join("", sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)).Select(x => x.ToString("X2")));
    }
    
    public static long ToUnixTime(this DateTime date)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);
    }
}