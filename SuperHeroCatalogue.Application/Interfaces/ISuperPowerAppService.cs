using System.Linq;
using SuperHeroCatalogue.Application.Models;

namespace SuperHeroCatalogue.Application.Interfaces
{
    public interface ISuperPowerAppService
    {
        SuperPowerModel GetSigle(int id);
        IQueryable<SuperPowerModel> GetAll();
        void Create(SuperPowerModel superPower);
        void Update(SuperPowerModel superPower);
        void Delete(int id);
    }
}