using System.Security.Cryptography;
using System.Text;

namespace BLL;

public static class Utilities
{
    public static string Sha1(this string stringToHash)
    {
        using var sha1 = SHA1.Create();
        return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
    }
    
    public static long ToUnixTime(this DateTime date)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);
    }
}