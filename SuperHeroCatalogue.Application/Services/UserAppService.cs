using AutoMapper;
using SuperHeroCatalogue.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Utils;
using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Application.Services
{
    public class UserAppService : IUserAppService 
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public UserAppService()
        {
        }

        public UserModel GetSigle(int id)
        {
            return Mapper.Map<User, UserModel>(_userService.GetSigle(id));
        }

        public IList<UserModel> GetAll()
        {
            Mapper.CreateMap<User, UserModel>();

            var lst = Mapper.Map<IList<User>, IList<UserModel>>(_userService.GetAll());

            return lst;
        }

        public void Create(UserModel user)
        {
            if (user.IdRole != 1 && user.IdRole != 2) throw new Exception("Invalid Role");


            Mapper.CreateMap<User, UserModel>();
            var lstUser = _userService.GetAll();
            var userBd = Mapper.Map<User, UserModel>(lstUser.FirstOrDefault(x => x.UserName == user.UserName));

            if (userBd != null) throw new Exception("UserName already exist");

            var pm = new PasswordManager();

            string salt;

            user.PasswordHash = pm.GeneratePasswordHash(user.PasswordHash + SaltProvider.GetSaltString(), out salt);

            user.Salt = salt;

            Mapper.CreateMap<UserModel, User>();
            _userService.Create(Mapper.Map<UserModel, User>(user));
        }

        public void Update(UserModel user)
        {
            Mapper.CreateMap<UserModel, User>();

            var userBd = _userService.GetSigle(user.Id);

            if (userBd == null) return;
            if (user.IdRole != 1 && user.IdRole != 2) return;

            var pm = new PasswordManager();

            string salt;
            user.PasswordHash = pm.GeneratePasswordHash(user.PasswordHash + userBd.Salt, out salt);
            user.Salt = salt;

            _userService.Update(Mapper.Map<UserModel, User>(user));
        }

        public void Delete(int id)
        {
            _userService.Delete(id);
        }

        public void Dispose()
        {
            _userService.Dispose();
        }
    }
}