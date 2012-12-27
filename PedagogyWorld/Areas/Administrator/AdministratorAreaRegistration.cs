using System.Web.Mvc;

namespace PedagogyWorld.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrator_default",
                "Administrator/{controller}/{action}/{id}",
                new { controller = "Static", action = "Index", id = UrlParameter.Optional },
                new[] { "PedagogyWorld.Areas.Administrator.Controllers" }
            );
        }
    }
}
