using System.Linq;
using SuperHeroCatalogue.Application.Models;

namespace SuperHeroCatalogue.Application.Interfaces
{
    public interface ISuperHeroAppService
    {
        SuperHeroModel GetSigle(int id);
        IQueryable<SuperHeroModel> GetAll();
        void Create(SuperHeroModel superHero);
        void CreateProtectionArea(ProtectionAreaModel protectionArea);
        ProtectionAreaModel GetLastProtectionArea();
        void Update(SuperHeroModel superHero);
        void Delete(int id);
    }
}