using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectDependencyResolver dependencyResovler = new NinjectDependencyResolver();
            dependencyResovler.Register<ResourceReader, DefaultResourceReader>();
            DependencyResolver.SetResolver(dependencyResovler);
        }

        protected void Application_BeginRequest()
        {
            HttpContextBase contextWrapper =new HttpContextWrapper(HttpContext.Current);
            string culture = RouteTable.Routes.GetRouteData(contextWrapper).Values["culture"] as string;
            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    CultureInfo cultureInfo = new CultureInfo(culture);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
                catch { }
            }
        }
    }
}