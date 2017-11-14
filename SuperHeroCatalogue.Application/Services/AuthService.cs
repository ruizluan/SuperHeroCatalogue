using System.Linq;
using AutoMapper;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Application.Utils;
using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public bool AuthenticateUser(string userName, string password, out UserModel user)
        {
            Mapper.CreateMap<User, UserModel>();

            var lstUser = _userService.GetAll();
            user = Mapper.Map<User, UserModel>(lstUser.FirstOrDefault(x => x.UserName == userName));

            if (user == null) return false;

            var pm = new PasswordManager();

            return pm.IsPasswordMatch(password, user.Salt, user.PasswordHash);
        }
    }
}