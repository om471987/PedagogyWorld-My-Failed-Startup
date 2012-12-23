using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.FileManager;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

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
            return View();
        }

        public ActionResult UnitOverview()
        {
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
