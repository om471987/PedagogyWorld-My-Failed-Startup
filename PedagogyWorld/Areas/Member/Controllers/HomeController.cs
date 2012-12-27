using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.FileManager;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using PedagogyWorld.Data;
using PedagogyWorld.Models;
using PedagogyWorld.Domain;

namespace PedagogyWorld.Areas.Member.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AzureBlobStorageService _azureBlobStorageService = new AzureBlobStorageService();

        public ActionResult TakeATour()
        {
            return View();
        }

        public ActionResult MyUnits()
        {
            //var db = new Context();
            //var unitCollection = db.Units;
            //ViewBag.UnitCollection = unitCollection;
            return View();
        }

        public ActionResult NewUnit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewUnit(int a)
        {
            //var db = new Context();
            //var unit = new Unit { 
            //Name = model.Name,
            //Description = model.Description,
            //UserId = db.Users.Where(t => t.UserName == User.Identity.Name)
            //                      .Select(t => t.Id)
            //                      .FirstOrDefault(),
            //Subject = new Subject { Name = model.Subject },
            //Grade = new Grade { Name = model.Grade },
            //};

            //try
            //{
            //    db.Units.Add(unit);
            //    db.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            return RedirectToAction("MyUnits");
        }

        public ActionResult UnitOverview(Guid id)
        {
            //var db = new Context();
            //var unit = db.Units.First(t => t.Id == id);
            //ViewBag.Grade = unit.Grade.ToString();
            //ViewBag.Subject = unit.Subject.ToString();
            //ViewBag.Unit = unit;
            return View();
        }

        public ActionResult planner()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult AddFiles()
        {
            var asds = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public ActionResult AddFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {

                    var blobContainer = _azureBlobStorageService.GetCloudBlobContainer(User.Identity.Name);
                    var blob = blobContainer.GetBlobReference(file.FileName);
                    blob.UploadFromStream(file.InputStream);
                }
            }
            return View();
        }

        public FilePathResult DownloadFile(string filePath)
        {
            return File(filePath, GetMimeType(filePath));
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext); // henter info fra windows registry
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            else if (ext == ".png") // a couple of extra info, due to missing information on the server
            {
                mimeType = "image/png";
            }
            else if (ext == ".flv")
            {
                mimeType = "video/x-flv";
            }
            return mimeType;
        }
    }
}
