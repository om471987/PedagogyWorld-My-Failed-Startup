using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld;

namespace PedagogyWorld.Areas.Admin.Controllers
{   
    public class StandardController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Standard/

        public ViewResult Index()
        {
            return View(context.Standards.ToList());
        }

        //
        // GET: /Standard/Details/5

        public ViewResult Details(int id)
        {
            Standard standard = context.Standards.Single(x => x.Id == id);
            return View(standard);
        }

        //
        // GET: /Standard/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Standard/Create

        [HttpPost]
        public ActionResult Create(Standard standard)
        {
            if (ModelState.IsValid)
            {
                context.Standards.Add(standard);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(standard);
        }
        
        //
        // GET: /Standard/Edit/5
 
        public ActionResult Edit(int id)
        {
            Standard standard = context.Standards.Single(x => x.Id == id);
            return View(standard);
        }

        //
        // POST: /Standard/Edit/5

        [HttpPost]
        public ActionResult Edit(Standard standard)
        {
            if (ModelState.IsValid)
            {
                context.Entry(standard).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(standard);
        }

        //
        // GET: /Standard/Delete/5
 
        public ActionResult Delete(int id)
        {
            Standard standard = context.Standards.Single(x => x.Id == id);
            return View(standard);
        }

        //
        // POST: /Standard/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Standard standard = context.Standards.Single(x => x.Id == id);
            context.Standards.Remove(standard);
            context.SaveChanges();
            return RedirectToAction("Index");
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