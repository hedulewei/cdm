using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CDMservers.Models;

[assembly:log4net.Config.XmlConfigurator(Watch=true)]
namespace CDMservers
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            
            //// EF DbContext
            //builder.RegisterType<CityData>().As<DbContext>().InstancePerLifetimeScope(); //InstancePerApiRequest
            //// Get your HttpConfiguration.
            //var config = GlobalConfiguration.Configuration;

            //// Register your Web API controllers.
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //// Set the dependency resolver to be Autofac.
            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
