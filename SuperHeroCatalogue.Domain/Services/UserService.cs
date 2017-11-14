using SuperHeroCatalogue.Domain.Entities;
using System;
using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public IList<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public User GetSigle(int id)
        {
            return _userRepository.GetSigle(id);
        }

        public void Update(User user)
        {
            _userRepository.Create(user);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
