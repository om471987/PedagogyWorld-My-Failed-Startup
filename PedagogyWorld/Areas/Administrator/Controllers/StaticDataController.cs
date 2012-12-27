using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Data;

namespace PedagogyWorld.Areas.Administrator.Controllers
{
    public class StaticDataController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Administrator/StaticData/

        public ActionResult Index()
        {
            return View(db.StaticDatas.ToList());
        }

        //
        // GET: /Administrator/StaticData/Details/5

        public ActionResult Details(int id = 0)
        {
            StaticData staticdata = db.StaticDatas.Find(id);
            if (staticdata == null)
            {
                return HttpNotFound();
            }
            return View(staticdata);
        }

        //
        // GET: /Administrator/StaticData/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administrator/StaticData/Create

        [HttpPost]
        public ActionResult Create(StaticData staticdata)
        {
            if (ModelState.IsValid)
            {
                db.StaticDatas.Add(staticdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staticdata);
        }

        //
        // GET: /Administrator/StaticData/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StaticData staticdata = db.StaticDatas.Find(id);
            if (staticdata == null)
            {
                return HttpNotFound();
            }
            return View(staticdata);
        }

        //
        // POST: /Administrator/StaticData/Edit/5

        [HttpPost]
        public ActionResult Edit(StaticData staticdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staticdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staticdata);
        }

        //
        // GET: /Administrator/StaticData/Delete/5

        public ActionResult Delete(int id = 0)
        {
            StaticData staticdata = db.StaticDatas.Find(id);
            if (staticdata == null)
            {
                return HttpNotFound();
            }
            return View(staticdata);
        }

        //
        // POST: /Administrator/StaticData/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StaticData staticdata = db.StaticDatas.Find(id);
            db.StaticDatas.Remove(staticdata);
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