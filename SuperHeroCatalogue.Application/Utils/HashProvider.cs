using System;
using System.Security.Cryptography;

namespace SuperHeroCatalogue.Application.Utils
{
    public class HashProvider
    {
        public static string Get(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            var byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            var byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}