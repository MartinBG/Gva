using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string CalculateSHA1(this string str)
        {
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] hashBytes = cryptoTransformSHA1.ComputeHash(Encoding.UTF8.GetBytes(str));

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}
