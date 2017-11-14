using System;
using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Services
{
    public interface IUserService : IDisposable
    {
        IList<User> GetAll();
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        User GetSigle(int id);
    }
}