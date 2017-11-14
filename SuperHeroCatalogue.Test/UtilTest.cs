using System.IO;

namespace SuperHeroCatalogue.Test
{
    public static class UtilTest
    {
        public static string GetRandomString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }
    }
}
