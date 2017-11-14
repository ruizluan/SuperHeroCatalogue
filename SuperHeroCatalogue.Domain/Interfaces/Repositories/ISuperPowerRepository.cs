using SuperHeroCatalogue.Domain.Entities;
using System.Collections.Generic;

namespace SuperHeroCatalogue.Domain.Interfaces.Repositories
{
    public interface ISuperPowerRepository
    {
        IList<SuperPower> GetAll();
        void Create(SuperPower superPower);
        void Delete(int id);
        SuperPower GetSigle(int id);
    }
}