using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    public class StandardController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Admin/Standard/

        public ActionResult Index()
        {
            var standards = db.Standards.Include(s => s.Header);
            return View(standards.ToList());
        }

        //
        // GET: /Admin/Standard/Details/5

        public ActionResult Details(int id = 0)
        {
            Standard standard = db.Standards.Find(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        //
        // GET: /Admin/Standard/Create

        public ActionResult Create()
        {
            ViewBag.Header_Id = new SelectList(db.Headers, "Id", "Header1");
            return View();
        }

        //
        // POST: /Admin/Standard/Create

        [HttpPost]
        public ActionResult Create(Standard standard)
        {
            if (ModelState.IsValid)
            {
                db.Standards.Add(standard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Header_Id = new SelectList(db.Headers, "Id", "Header1", standard.Header_Id);
            return View(standard);
        }

        //
        // GET: /Admin/Standard/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Standard standard = db.Standards.Find(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            ViewBag.Header_Id = new SelectList(db.Headers, "Id", "Header1", standard.Header_Id);
            return View(standard);
        }

        //
        // POST: /Admin/Standard/Edit/5

        [HttpPost]
        public ActionResult Edit(Standard standard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Header_Id = new SelectList(db.Headers, "Id", "Header1", standard.Header_Id);
            return View(standard);
        }

        //
        // GET: /Admin/Standard/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Standard standard = db.Standards.Find(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        //
        // POST: /Admin/Standard/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Standard standard = db.Standards.Find(id);
            db.Standards.Remove(standard);
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