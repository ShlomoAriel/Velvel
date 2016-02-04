using System;
using System.Data.Entity;
using IdentitySample.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Velvel.Domain.Data;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Services;
using Velvel.Domain.Tables.Plumbings;
using Velvel.Domain.Users;
using Velvel.Domain.Users.Customers;

namespace Velvel.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here

            container.RegisterType<DbContext, ApplicationDbContext>(
    new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IRoleStore<IdentityRole,string>, RoleStore<IdentityRole>>(
                new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(
                new InjectionConstructor());


            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IManagerService, ManagerService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IPlumbingService, PlumbingService>();
            container.RegisterType<IFlooringService, FlooringService>();
            container.RegisterType(typeof(IFlooringService), typeof(FlooringService));
            container.RegisterType(typeof(ITableService<>), typeof(TableService<>));
            container.RegisterType(typeof(IProjectEntityService<>), typeof(ProjectEntityService<>));
            container.RegisterType(typeof(IPricedEntityService<>), typeof(PricedEntityService<>));
            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
