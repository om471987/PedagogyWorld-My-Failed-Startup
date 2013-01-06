using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Data;
//using PedagogyWorld.FileStorage;
using PedagogyWorld.Models;
using PedagogyWorld.ExtensionMethod;

namespace PedagogyWorld.Controllers
{

    public class FileEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public double start { get; set; }
        public string url { get; set; }
        public int Date { get; set; }
    }

    [Authorize]
    public class FilesController : Controller
    {
        private Context context = new Context();
        //private FileManager _fileManager = new FileManager();
        //
        // GET: /Files/

        public ViewResult Index()
        {
            return View(context.Files.Include(file => file.FileTypes).Include(file => file.Units).Include(file => file.TeachingDates).ToList());
        }

        //
        // GET: /Files/Details/5

        public ViewResult Details(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            return View(file);
        }

        //
        // GET: /Files/Create

        public ActionResult Create(Guid unitId, int fileTypeId)
        {
            ViewBag.UnitId = unitId;
            ViewBag.FileTypes = context.FileTypes;
            return View();
        } 

        //
        // POST: /Files/Create

        [HttpPost]
        public ActionResult Create(FileModel fileModel, IEnumerable<HttpPostedFileBase> files)
        {
            var path = string.Empty;
            var user = User.Identity.Name == "" ? "omkar" : User.Identity.Name;
            foreach (var file in files)
            {
                if (file.ContentLength <= 0) continue;
                //var blobContainer = _fileManager.GetCloudBlobContainer(user);
                //var blob = blobContainer.GetBlobReference(file.FileName);
                //blob.UploadFromStream(file.InputStream);
                //path = blob.Uri.ToString();
            }
            if (ModelState.IsValid)
            {
                var file = new File
                    {
                        Name = fileModel.Name,
                        Path = path,
                        Id = Guid.NewGuid()
                    };
                context.Files.Add(file);
                var unit = context.Units.Find(fileModel.UnitId);
                unit.Files = new Collection<File> {file};
                file.Units = new Collection<Unit> { unit };

                var fileType = context.FileTypes.Find(fileModel.FileType);
                fileType.Files = new Collection<File> { file };
                file.FileTypes = new Collection<FileType> { fileType };

                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(fileModel);
        }
        
        //
        // GET: /Files/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            return View(file);
        }

        //
        // POST: /Files/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                context.Entry(file).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        //
        // GET: /Files/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            return View(file);
        }

        //
        // POST: /Files/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            context.Files.Remove(file);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Planner()
        {
            //ViewBag.Files = context.Files;

            ViewBag.Files = new[]
                { "event3", "Event4"
                };
            return View();
        }

        [AllowAnonymous]
        public ActionResult JSonPlanner(double start, double end)
        {
            start.ToDateTime();

            ViewBag.Files = context.Files;

            var title = new[]
                {
                    new
                        {
                            id = 111,
                            title = "event1",
                            start = DateTime.Now.ToUnixTimeStamp(),
                            url = "http://yahoo.com/"
                        },
                    new
                        {
                            id = 222,
                            title = "Event2",
                            start =  DateTime.Now.AddDays(4).ToUnixTimeStamp(),
                            url = "http://yahoo.com/"
                        },
                };
            return Json(title, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}