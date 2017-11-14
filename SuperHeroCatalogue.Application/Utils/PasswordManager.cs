namespace SuperHeroCatalogue.Application.Utils
{
    public class PasswordManager
    {
        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = SaltProvider.GetSaltString();
            var finalString = plainTextPassword + salt;
            return HashProvider.Get(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            var finalString = password + salt;
            return hash == HashProvider.Get(finalString);
        }
    }
}