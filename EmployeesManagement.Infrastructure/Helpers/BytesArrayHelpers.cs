using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EmployeesManagement.Infrastructure.Helpers
{
    public static class BytesArrayHelpers
    {
        public static string ToSHA256Hash(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : Encoding.ASCII.GetBytes(str).ToSHA256Hash();
        }

        public static string ToSHA256Hash(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            using (var md5 = SHA256.Create())
            {
                return string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
            }
        }
    }
}
