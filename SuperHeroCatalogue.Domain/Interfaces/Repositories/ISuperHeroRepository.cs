using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Repositories
{
    public interface ISuperHeroRepository
    {
        IList<SuperHero> GetAll();
        void Create(SuperHero superHero);
        void CreateProtectionArea(ProtectionArea protectionArea);
        ProtectionArea GetLastProtectionArea();
        void Delete(int id);
        SuperHero GetSigle(int id);
    }
}