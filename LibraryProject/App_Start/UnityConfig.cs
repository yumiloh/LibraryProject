using LibraryProject.DataAccess;
using LibraryProject.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace LibraryProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IBorrowerRepository, BorrowerRepository>();
            container.RegisterType<ILibraryRepository, LibraryRepository>();
            container.RegisterType<IManagerRepository, ManagerRepository>();
            container.RegisterType<LibraryContext, LibraryContext>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}