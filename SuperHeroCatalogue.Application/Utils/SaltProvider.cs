using System;
using System.Security.Cryptography;

namespace SuperHeroCatalogue.Application.Utils
{
    public static class SaltProvider
    {
        private static readonly RNGCryptoServiceProvider MCryptoServiceProvider;
        private const int SaltSize = 24;

        static SaltProvider()
        {
            MCryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            var saltBytes = new byte[SaltSize];

            MCryptoServiceProvider.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }
    }
}