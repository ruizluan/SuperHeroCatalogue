using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Application.Services
{
    public class SuperPowerAppService : ISuperPowerAppService //AppServiceBase<Infra.Data.Context.SuperHeroCatalogue>, 
    {
        private readonly ISuperPowerService _superPowerService;

        public SuperPowerAppService(ISuperPowerService superPowerService)
        {
            _superPowerService = superPowerService;
        }

        public SuperPowerModel GetSigle(int id)
        {
            return Mapper.Map<SuperPower, SuperPowerModel>(_superPowerService.GetSigle(id));
        }

        public IQueryable<SuperPowerModel> GetAll()
        {
            Mapper.CreateMap<SuperPower, SuperPowerModel>();

            var res = Mapper.Map<IList<SuperPower>, IList<SuperPowerModel>>(_superPowerService.GetAll());

            return res.AsQueryable();
        }

        public void Create(SuperPowerModel superPower)
        {
            Mapper.CreateMap<SuperPowerModel, SuperPower>();

            _superPowerService.Create(Mapper.Map<SuperPowerModel, SuperPower>(superPower));
        }

        public void Update(SuperPowerModel superPower)
        {
            Mapper.CreateMap<SuperPowerModel, SuperPower>();

            _superPowerService.Update(Mapper.Map<SuperPowerModel, SuperPower>(superPower));
        }

        public void Delete(int id)
        {
            var superPower = _superPowerService.GetSigle(id);

            if (superPower.IdSuperHero == 0)
            {
                _superPowerService.Delete(id);
            }
        }

        public void Dispose()
        {
            _superPowerService.Dispose();
        }
    }
}