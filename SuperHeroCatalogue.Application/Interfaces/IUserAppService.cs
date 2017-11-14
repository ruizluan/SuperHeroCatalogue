using System.Collections.Generic;
using SuperHeroCatalogue.Application.Models;

namespace SuperHeroCatalogue.Application.Interfaces
{
    public interface IUserAppService
    {
        UserModel GetSigle(int id);
        IList<UserModel> GetAll();
        void Create(UserModel user);
        void Update(UserModel user);
        void Delete(int id);
    }
}