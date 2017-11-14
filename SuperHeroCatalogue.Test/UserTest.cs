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
    public class UserTest
    {
        private static readonly IKernel Kernel = new StandardKernel(new NinjectModulo());
        private readonly IUserAppService _userAppService = Kernel.Get<IUserAppService>();

        [TestMethod, TestCategory("Unit")]
        public void GetAll()
        {
            try
            {
                _userAppService.GetAll();
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
            var user = new UserModel
            {
                UserName = UtilTest.GetRandomString()
                ,IdRole = 2
                ,PasswordHash = UtilTest.GetRandomString()
            };

            try
            {
                _userAppService.Create(user);
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
            var userId = _userAppService.GetAll().OrderByDescending(x => x.Id).Select(x => x.Id).First();

            var user = new UserModel
            {
                Id = userId
                ,PasswordHash = UtilTest.GetRandomString()
            };

            try
            {
                _userAppService.Update(user);
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
                var userId = _userAppService.GetAll().OrderByDescending(x => x.Id).Select(x => x.Id).First();
                _userAppService.GetSigle(userId);
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
           var userId = _userAppService.GetAll().OrderByDescending(x=>x.Id).Select(x=>x.Id).First();
            _userAppService.Delete(userId);
        }
    }
}