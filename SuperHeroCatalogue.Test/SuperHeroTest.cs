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
    public class SuperHeroTest
    {
        private static readonly IKernel Kernel = new StandardKernel(new NinjectModulo());
        private readonly ISuperHeroAppService _superHeroAppService = Kernel.Get<ISuperHeroAppService>();

        [TestMethod, TestCategory("Unit")]
        public void GetAll()
        {
            try
            {
                _superHeroAppService.GetAll();
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
            _superHeroAppService.CreateProtectionArea(
                new ProtectionAreaModel {Lat = -23561414,Long = -466558816 ,Name = UtilTest.GetRandomString(), Radius = 15});

            var superHero = new SuperHeroModel
            {
                Name = UtilTest.GetRandomString()
                ,Alias = UtilTest.GetRandomString()
                ,IdProtectionArea = _superHeroAppService.GetLastProtectionArea().Id
            };

            try
            {
                _superHeroAppService.Create(superHero);
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
            var superHero = _superHeroAppService.GetAll().OrderByDescending(x => x.Id).First();

            superHero.Alias = UtilTest.GetRandomString();

            try
            {
                _superHeroAppService.Update(superHero);
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
                var superHeroId = _superHeroAppService.GetAll().OrderByDescending(x => x.Id).Select(x => x.Id).First();
                _superHeroAppService.GetSigle(superHeroId);
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
            var superHeroId = _superHeroAppService.GetAll().OrderByDescending(x => x.Id).Select(x => x.Id).First();
            _superHeroAppService.Delete(superHeroId);
        }
    }
}