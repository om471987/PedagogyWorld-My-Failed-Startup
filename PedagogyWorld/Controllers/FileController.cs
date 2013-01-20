using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using PedagogyWorld.FileStorage;
using PedagogyWorld.Models;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private Context db = new Context();

        public ActionResult Planner()
        {
            return View();
        }

        public ContentResult ListBuckets()
        {
            var aws = new AwsHandle();
            var serializer = new JavaScriptSerializer();
            return Content(serializer.Serialize(aws.ListBuckets()), "application/json");
        }

        public ContentResult ListObjects(string id)
        {
            var aws = new AwsHandle();
            var serializer = new JavaScriptSerializer();
            return Content(serializer.Serialize(aws.GetDocs(id)), "application/json");
        }

        public ActionResult DownloadFile(Guid id)
        {
            var file = db.Files.FirstOrDefault(t=>t.Id == id);

            if (file.UserProfile_Id == (int)Membership.GetUser().ProviderUserKey)
            {
                var aws = new AwsHandle();

                var stream = aws.DownloadObject("pedagogyworld", file.StoragePath);
                return File(stream, file.ContentType, file.FileName);
            }
            return Content("");
        }

        public ActionResult Index(Guid id)
        {
            var unit = db.Units.FirstOrDefault(t => t.Id == id);

            if (unit.UserProfile_Id == (int)Membership.GetUser().ProviderUserKey)
            {
                var files = from f in db.UnitFiles.Where(t => t.Unit_Id == id) select f.File;
                return View(files.ToList());    
            }
            return Content(User.Identity.Name);
        }

        //
        // GET: /File/Details/5

        public ActionResult Details(Guid id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            var model = new FileModel
            {
                TeachingDate=DateTime.Now.Date
            };

            var result = new List<SelectListItem>();
            foreach (var t in db.FileTypes)
            {
                result.Add(new SelectListItem
                {
                    Text = t.FileTypeName,
                    Value = t.Id.ToString()
                });
            }
            model.FileTypes = result.ToList();
            return View(model);
        }

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(FileModel fileModel)
        {
            if (ModelState.IsValid && fileModel.UploadFile != null)
            {
                var fileId = Guid.NewGuid();
                var aws = new AwsHandle();
                var result = aws.NewFile("pedagogyworld", fileId.ToString("N"), fileModel.UploadFile.InputStream, fileModel.UploadFile.ContentType);
                if (result)
                {
                    var file = new File
                        {
                            Id = fileId,
                            ContentType = fileModel.UploadFile.ContentType,
                            ContentLength = fileModel.UploadFile.ContentLength,
                            FileName = fileModel.UploadFile.FileName,
                            StoragePath = fileId.ToString("N"),
                            UserProfile_Id = (int) Membership.GetUser().ProviderUserKey
                        };
                    db.Files.Add(file);

                    foreach (var t in fileModel.FileIds)
                    {
                        var type = new FileFileType
                        {
                            File_Id = fileId,
                            FileType_Id = t
                        };
                        db.FileFileTypes.Add(type);
                    }

                    var unit = new UnitFile
                    {
                        File_Id = fileId,
                        Unit_Id = fileModel.Id
                    };

                    db.UnitFiles.Add(unit);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Unit", new { fileModel.Id });
            }
            var typeList = new List<SelectListItem>();
            foreach (var t in db.FileTypes)
            {
                typeList.Add(new SelectListItem
                {
                    Text = t.FileTypeName,
                    Value = t.Id.ToString()
                });
            }
            fileModel.FileTypes = typeList.ToList();
            return View(fileModel);
        }

        public ActionResult Edit(Guid id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        //
        // GET: /File/Delete/5

        public ActionResult Delete(Guid id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        //
        // POST: /File/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            File file = db.Files.Find(id);
            db.Files.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}