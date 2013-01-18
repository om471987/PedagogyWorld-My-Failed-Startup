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
    public class SubjectController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Subject/

        public ViewResult Index()
        {
            return View(context.Subjects.Include(subject => subject.Units).ToList());
        }

        //
        // GET: /Subject/Details/5

        public ViewResult Details(int id)
        {
            Subject subject = context.Subjects.Single(x => x.Id == id);
            return View(subject);
        }

        //
        // GET: /Subject/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Subject/Create

        [HttpPost]
        public ActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                context.Subjects.Add(subject);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(subject);
        }
        
        //
        // GET: /Subject/Edit/5
 
        public ActionResult Edit(int id)
        {
            Subject subject = context.Subjects.Single(x => x.Id == id);
            return View(subject);
        }

        //
        // POST: /Subject/Edit/5

        [HttpPost]
        public ActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                context.Entry(subject).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        //
        // GET: /Subject/Delete/5
 
        public ActionResult Delete(int id)
        {
            Subject subject = context.Subjects.Single(x => x.Id == id);
            return View(subject);
        }

        //
        // POST: /Subject/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = context.Subjects.Single(x => x.Id == id);
            context.Subjects.Remove(subject);
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