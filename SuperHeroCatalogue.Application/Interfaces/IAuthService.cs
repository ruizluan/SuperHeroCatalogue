using SuperHeroCatalogue.Application.Models;

namespace SuperHeroCatalogue.Application.Interfaces
{
    public interface IAuthService
    {
        bool AuthenticateUser(string userName, string password, out UserModel user);
    }
}