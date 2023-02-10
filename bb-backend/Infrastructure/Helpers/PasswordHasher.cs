using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helpers
{
    public class PasswordHasher
    {
        public static string GetPasswordHash(string pass)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
            var hash = BitConverter.ToString(hashedBytes).ToLower();
            return hash;
        } 
    }
}
