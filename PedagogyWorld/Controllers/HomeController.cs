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
            ViewBag.Message = "Your app description page.";
            var url = "http://localhost:20000/File/Planner";
            var filename = @"C:\Users\omkar\Desktop\t1.pdf";
            var a = new HtmlToPdf();
            if (a.Convert(url, filename))
            {
                Thread.Sleep(6000);
                var file = System.IO.File.ReadAllText(filename);
                return File(Encoding.UTF8.GetBytes(file), "application/pdf", "t2.pdf");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
    }
}
