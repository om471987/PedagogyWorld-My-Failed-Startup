using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Domain;
using PedagogyWorld.Data;

namespace PedagogyWorld.Areas.Administrator.Controllers
{
    public class FileTypeController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Administrator/FileType/

        public ActionResult Index()
        {
            return View(db.FileTypes.ToList());
        }

        //
        // GET: /Administrator/FileType/Details/5

        public ActionResult Details(Guid id)
        {
            FileType filetype = db.FileTypes.Find(id);
            if (filetype == null)
            {
                return HttpNotFound();
            }
            return View(filetype);
        }

        //
        // GET: /Administrator/FileType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administrator/FileType/Create

        [HttpPost]
        public ActionResult Create(FileType filetype)
        {
            if (ModelState.IsValid)
            {
                filetype.Id = Guid.NewGuid();
                db.FileTypes.Add(filetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(filetype);
        }

        //
        // GET: /Administrator/FileType/Edit/5

        public ActionResult Edit(Guid id)
        {
            FileType filetype = db.FileTypes.Find(id);
            if (filetype == null)
            {
                return HttpNotFound();
            }
            return View(filetype);
        }

        //
        // POST: /Administrator/FileType/Edit/5

        [HttpPost]
        public ActionResult Edit(FileType filetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filetype);
        }

        //
        // GET: /Administrator/FileType/Delete/5

        public ActionResult Delete(Guid id)
        {
            FileType filetype = db.FileTypes.Find(id);
            if (filetype == null)
            {
                return HttpNotFound();
            }
            return View(filetype);
        }

        //
        // POST: /Administrator/FileType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FileType filetype = db.FileTypes.Find(id);
            db.FileTypes.Remove(filetype);
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