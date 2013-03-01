using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Newtonsoft.Json;
using PedagogyWorld.ExtensionMethod;
using PedagogyWorld.Services;
using PedagogyWorld.Models;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private Context db = new Context();

        public ContentResult ListBuckets()
        {
            var aws = new FileHandle();
            var serializer = new JavaScriptSerializer();
            return Content(serializer.Serialize(aws.ListBuckets()), "application/json");
        }

        public ContentResult ListObjects(string id)
        {
            var aws = new FileHandle();
            var serializer = new JavaScriptSerializer();
            return Content(serializer.Serialize(aws.GetDocs(id)), "application/json");
        }

        public ActionResult DownloadFile(Guid id)
        {
            var file = db.Files.FirstOrDefault(t=>t.Id == id);

            if (file.UserProfile_Id == (int)Membership.GetUser().ProviderUserKey)
            {
                var aws = new FileHandle();

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

        public ActionResult Details(Guid id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

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

        [HttpPost]
        public ActionResult Create(FileModel fileModel)
        {
            if (ModelState.IsValid && fileModel.UploadFile != null)
            {
                var fileId = Guid.NewGuid();
                var aws = new FileHandle();
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


        public ActionResult Planner()
        {
            var userId = (int) Membership.GetUser().ProviderUserKey;

            var list = new List<PlannerModel>();
            foreach (var t in db.Files.Where(t => t.UserProfile_Id == userId))
            {
                list.Add(new PlannerModel{Id = t.Id,FileName = t.FileName});
            }

            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            var files = (from f in db.Files.AsEnumerable()
                         join t in db.TeachingDates on f.Id equals t.File_Id
                         where t.StartDate >= startDate &&
                        t.EndDate < endDate &&
                        f.UserProfile_Id == userId
                         select new { id = f.Id, title = f.FileName, start = t.StartDate.ToUnixTimeStamp(), end = t.EndDate.ToUnixTimeStamp() }).ToList();
            ViewBag.Files = JsonConvert.SerializeObject(files);
            return View(list);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddPlannerFile(Guid id, int month, int year, int day)
        {
            var s=new StringBuilder();
            s.Append((month + 1) < 10 ? "0" : "");
            s.Append(month + 1);
            s.Append("-");
            s.Append((day) < 10 ? "0" : "");
            s.Append(day);
            s.Append("-");
            s.Append(year + 1900);
            var date = DateTime.ParseExact(s.ToString(), "MM-dd-yyyy", CultureInfo.InvariantCulture);
            try
            {
                var file = db.Files.FirstOrDefault(t => t.Id == id);
                var teachingDate = new TeachingDate
                {
                    StartDate = date,
                    EndDate = date,
                    File_Id = file.Id
                };
                db.TeachingDates.Add(teachingDate);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);    
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePlannerFile(Guid id, int startMonth, int startYear, int startDay, int endDay, int endMonth, int endYear)
        {
            var s = new StringBuilder();
            s.Append((startMonth + 1) < 10 ? "0" : "");
            s.Append(startMonth + 1);
            s.Append("-");
            s.Append((startDay) < 10 ? "0" : "");
            s.Append(startDay);
            s.Append("-");
            s.Append(startYear + 1900);
            var startDate = DateTime.ParseExact(s.ToString(), "MM-dd-yyyy", CultureInfo.InvariantCulture);

            var e = new StringBuilder();
            e.Append((endMonth + 1) < 10 ? "0" : "");
            e.Append(endMonth + 1);
            e.Append("-");
            e.Append((endDay) < 10 ? "0" : "");
            e.Append(endDay);
            e.Append("-");
            e.Append(endYear + 1900);
            var endDate = DateTime.ParseExact(e.ToString(), "MM-dd-yyyy", CultureInfo.InvariantCulture);

            try
            {
                var file = db.TeachingDates.FirstOrDefault(t => t.File_Id == id);
                file.StartDate = startDate;
                file.EndDate = endDate;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public ActionResult LoadPlanner(int month, int year)
        {
            var userId = (int) Membership.GetUser().ProviderUserKey;

            var startDate = new DateTime(year + 1900, month + 1, 1);
            var endDate = new DateTime(year + 1900, month + 1, DateTime.DaysInMonth(year + 1900, month + 1));

            var files = (from f in db.Files.AsEnumerable()
                          join t in db.TeachingDates on f.Id equals t.File_Id
                          where t.StartDate >= startDate &&
                         t.EndDate < endDate &&
                         f.UserProfile_Id == userId
                          select new { id=f.Id, title=f.FileName, start = t.StartDate.ToUnixTimeStamp(), end = t.EndDate.ToUnixTimeStamp() }).ToList();

            return Json(files, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult JSonPlanner(double start, double end)
        {
            //var userId = (int) Membership.GetUser().ProviderUserKey;
            //var files = (from f in db.Files.Where(t => t.UserProfile_Id == userId)
            //             select new
            //             {
            //                 id = f.Id,
            //                 title = f.FileName,
            //                 start = DateTime.Now,
            //                 end = DateTime.Now
            //             }).ToList();
            //start.ToDateTime();

            var files = new[]
                {
                    new
                        {
                            id = 111,
                            title = "event1",
                            start = DateTime.Now.ToUnixTimeStamp()
                        },
                    new
                        {
                            id = 222,
                            title = "Event2",
                            start =  DateTime.Now.AddDays(4).ToUnixTimeStamp()
                        }
                };
            return Json(files, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadZip(Guid id)
        {
            var file = db.Files.Select(t=>t.StoragePath).ToList();

            //if (file.UserProfile_Id == (int)Membership.GetUser().ProviderUserKey)
            //{
            //    var aws = new FileHandle();

            //    var stream = aws.DownloadObject("pedagogyworld", file.StoragePath);
            //    return File(stream, file.ContentType, file.FileName);
            //}

            return new ZipResult(file, "test.zip");
        }

        public ActionResult Delete(Guid id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

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