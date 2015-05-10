using EntechTestService.Contracts.Internal.Repositories;
using EntechTestService.InMemoryDb;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace EntechTestService.API.Host
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterInstance(new Db());
            container.RegisterInstance<IStoreRepository>(container.Resolve<Db>().StoreRepository);

            
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}