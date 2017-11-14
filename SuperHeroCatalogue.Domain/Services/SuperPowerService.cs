using SuperHeroCatalogue.Domain.Entities;
using System;
using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Domain.Services
{
    public class SuperPowerService : ISuperPowerService
    {
        private readonly ISuperPowerRepository _superPowerRepository;

        public SuperPowerService(ISuperPowerRepository superPowerRepository)
        {
            _superPowerRepository = superPowerRepository;
        }

        public void Delete(int id)
        {
            _superPowerRepository.Delete(id);
        }

        public SuperPower GetSigle(int id)
        {
            return _superPowerRepository.GetSigle(id);
        }

        public IList<SuperPower> GetAll()
        {
            return _superPowerRepository.GetAll();
        }

        public void Create(SuperPower superPower)
        {
            _superPowerRepository.Create(superPower);
        }

        public void Update(SuperPower superPower)
        {
            _superPowerRepository.Create(superPower);
        }

        public void Dispose()
        {
            //_userReportRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
