using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PedagogyWorld.App_Start;
using PedagogyWorld.Filters;

namespace PedagogyWorld
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            new InitializeSimpleMembershipAttribute().OnActionExecuting(null);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}