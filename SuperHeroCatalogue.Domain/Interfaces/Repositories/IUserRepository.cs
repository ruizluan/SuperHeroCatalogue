using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        void Create(User user);
        void Delete(int id);
        User GetSigle(int id);
    }
}