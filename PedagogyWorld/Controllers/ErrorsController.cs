using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedagogyWorld.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult OperationalError()
        {
            return View();
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}
