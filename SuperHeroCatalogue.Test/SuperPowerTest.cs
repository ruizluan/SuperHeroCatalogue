using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Infra.CrossCutting.IoC;

namespace SuperHeroCatalogue.Test
{
    [TestClass]
    public class SuperPowerTest
    {
        private static readonly IKernel Kernel = new StandardKernel(new NinjectModulo());
        private readonly ISuperPowerAppService _superPowerAppService = Kernel.Get<ISuperPowerAppService>();

        [TestMethod, TestCategory("Unit")]
        public void GetAll()
        {
            try
            {
                _superPowerAppService.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void Create()
        {
            var superPower = new SuperPowerModel
            {
                Name = UtilTest.GetRandomString()
                ,Description = UtilTest.GetRandomString()
                ,IdSuperHero = 1
            };

            try
            {
                _superPowerAppService.Create(superPower);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void Update()
        {
            var superPower = _superPowerAppService.GetAll().OrderByDescending(x => x.Id).First();

            superPower.Description = UtilTest.GetRandomString();

            try
            {
                _superPowerAppService.Update(superPower);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void GetSigle()
        {
            try
            {
                var superPowerId = _superPowerAppService.GetAll().OrderByDescending(x => x.Id).Select(x => x.Id).First();
                _superPowerAppService.GetSigle(superPowerId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void Delete()
        {
            var superPowerId = _superPowerAppService.GetAll().OrderByDescending(x => x.Id).Select(x => x.Id).First();
            _superPowerAppService.Delete(superPowerId);
        }
    }
}