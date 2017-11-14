using System;
using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Services
{
    public interface ISuperHeroService : IDisposable
    {
        IList<SuperHero> GetAll();
        void Create(SuperHero superHero);
        void CreateProtectionArea(ProtectionArea protectionArea);
        ProtectionArea GetLastProtectionArea();
        void Update(SuperHero superHero);
        void Delete(int id);
        SuperHero GetSigle(int id);
    }
}