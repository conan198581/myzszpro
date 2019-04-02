using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using RPZSZ.AdminWeb.Filters;
using RPZSZ.CommonMVC;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using RPZSZ.IService;

namespace RPZSZ.AdminWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region Autofac的配置
            var builder = new ContainerBuilder();
            //注册当前程序集中的所有controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //注册Service程序集 只注册t不是抽象的 并且 实现了 IServiceSupport 的 对象;
            Assembly[] assemblies = new Assembly[] { Assembly.Load("RPZSZ.Service")};
            builder.RegisterAssemblyTypes(assemblies).Where(t => !t.IsAbstract && typeof(IServiceSupport).IsAssignableFrom(t)).AsImplementedInterfaces().PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion


            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());
            log4net.Config.XmlConfigurator.Configure();


            /*
            GlobalFilters.Filters.Add(new ZSZExceptionFilter());
            GlobalFilters.Filters.Add(new JsonResultFilter());
            */
            FilterConfig.RegisterFilters(GlobalFilters.Filters);
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
