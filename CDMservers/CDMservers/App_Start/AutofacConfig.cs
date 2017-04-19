using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CDMservers.Models;

namespace CDMservers
{
    public class AutofacConfig
    {
        public static IContainer Container = null;

     

        public static void Initialize(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            RegisterTypes(builder);

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

         //   app.UseAutofacMiddleware(Container);

        }

        // to make sure registered types using autofac can be used in owin context,
        // types' instances should be created per lifetimescope, not per request
        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF DbContext
            builder.RegisterType<CityData>().As<DbContext>().InstancePerLifetimeScope(); //InstancePerApiRequest

            // Register repositories by using Autofac's OpenGenerics feature
            // More info: http://code.google.com/p/autofac/wiki/OpenGenerics
        //    builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        
      //      builder.RegisterType<MFUserService>().As<IMFUserService>().InstancePerLifetimeScope();
        }
    }
}