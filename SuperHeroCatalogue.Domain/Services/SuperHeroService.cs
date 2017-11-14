using SuperHeroCatalogue.Domain.Entities;
using System;
using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Domain.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly ISuperHeroRepository _superHeroRepository;

        public SuperHeroService(ISuperHeroRepository superHeroService)
        {
            _superHeroRepository = superHeroService;
        }

        public SuperHero GetSigle(int id)
        {
            return _superHeroRepository.GetSigle(id);
        }

        public IList<SuperHero> GetAll()
        {
            return _superHeroRepository.GetAll();
        }

        public void Create(SuperHero superHero)
        {
            _superHeroRepository.Create(superHero);
        }

        public void CreateProtectionArea(ProtectionArea protectionArea)
        {
            _superHeroRepository.CreateProtectionArea(protectionArea);
        }
        public ProtectionArea GetLastProtectionArea()
        {
            return _superHeroRepository.GetLastProtectionArea();
        }
        public void Update(SuperHero superHero)
        {
            _superHeroRepository.Create(superHero);
        }

        public void Delete(int id)
        {
            _superHeroRepository.Delete(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}