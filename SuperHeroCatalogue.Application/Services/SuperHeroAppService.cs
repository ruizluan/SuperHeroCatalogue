using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Application.Services
{
    public class SuperHeroAppService : ISuperHeroAppService
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroAppService(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        public SuperHeroModel GetSigle(int id)
        {
            Mapper.CreateMap<SuperHero, SuperHeroModel>();
            return Mapper.Map<SuperHero, SuperHeroModel>(_superHeroService.GetSigle(id));
        }

        public IQueryable<SuperHeroModel> GetAll()
        {
            Mapper.CreateMap<SuperHero, SuperHeroModel>();

            var res = Mapper.Map<IList<SuperHero>, IList<SuperHeroModel>>(_superHeroService.GetAll());

            return res.AsQueryable();
        }

        public void Create(SuperHeroModel superHero)
        {
            Mapper.CreateMap<SuperHeroModel, SuperHero>();

            _superHeroService.Create(Mapper.Map<SuperHeroModel, SuperHero>(superHero));
        }
        
        public void CreateProtectionArea(ProtectionAreaModel createProtectionArea)
        {
            Mapper.CreateMap<ProtectionAreaModel, ProtectionArea>();

            _superHeroService.CreateProtectionArea(Mapper.Map<ProtectionAreaModel, ProtectionArea>(createProtectionArea));
        }
        public ProtectionAreaModel GetLastProtectionArea()
        {
            Mapper.CreateMap<ProtectionArea, ProtectionAreaModel>();

           return Mapper.Map <ProtectionArea, ProtectionAreaModel>(_superHeroService.GetLastProtectionArea());
        }

        public void Update(SuperHeroModel superHero)
        {
            Mapper.CreateMap<SuperHeroModel, SuperHero>();

            _superHeroService.Update(Mapper.Map<SuperHeroModel, SuperHero>(superHero));
        }

        public void Delete(int id)
        {
            _superHeroService.Delete(id);
        }

        public void Dispose()
        {
            _superHeroService.Dispose();
        }
    }
}