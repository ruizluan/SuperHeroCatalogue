using Ninject.Modules;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Services;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Domain.Interfaces.Services;
using SuperHeroCatalogue.Domain.Services;
using SuperHeroCatalogue.Infra.Data.Repositories;

namespace SuperHeroCatalogue.Infra.CrossCutting.IoC
{
    public class NinjectModulo : NinjectModule
    {
        public override void Load()
        {
            // app
            Bind<IAuthService>().To<AuthService>();
            Bind<IDistributedCacheService>().To<DistributedCacheService>().InSingletonScope();
            Bind<IDateTimeProvider>().To<DateTimeProvider>();
            Bind <IUserAppService>().To<UserAppService>();
            Bind<ISuperPowerAppService>().To<SuperPowerAppService>();
            Bind<ISuperHeroAppService>().To<SuperHeroAppService>();
            Bind<IAuditAppService>().To<AuditAppService>();

            // service
            Bind<IAuditService>().To<AuditService>();
            Bind<IUserService>().To<UserService>();
            Bind<ISuperPowerService>().To<SuperPowerService>();
            Bind<ISuperHeroService>().To<SuperHeroService>();

            // data repos
            Bind<IAuditRepository>().To<AuditRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ISuperHeroRepository>().To<SuperHeroRepository>();
            Bind<ISuperPowerRepository>().To<SuperPowerRepository>();
        }
    }
}