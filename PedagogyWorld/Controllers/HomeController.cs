using System;
using PedagogyWorld.Services;
using System.Text;
using System.Web.Mvc;
using System.Threading;

namespace PedagogyWorld.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        [Authorize]
        public ActionResult TakeATour()
        {
            return View();
        }
    }
}
