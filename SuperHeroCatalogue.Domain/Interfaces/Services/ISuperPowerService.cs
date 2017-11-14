using System;
using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Services
{
    public interface ISuperPowerService : IDisposable
    {
        IList<SuperPower> GetAll();
        void Create(SuperPower superPower);
        void Update(SuperPower superPower);
        void Delete(int id);
        SuperPower GetSigle(int id);
    }
}