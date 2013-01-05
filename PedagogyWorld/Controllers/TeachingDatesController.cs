using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Data;
using PedagogyWorld.Models;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class TeachingDatesController : Controller
    {
        private Context context = new Context();

        //
        // GET: /TeachingDates/

        public ViewResult Index()
        {
            return View(context.TeachingDates.ToList());
        }

        //
        // GET: /TeachingDates/Details/5

        public ViewResult Details(int id)
        {
            TeachingDate teachingdate = context.TeachingDates.Single(x => x.Id == id);
            return View(teachingdate);
        }

        //
        // GET: /TeachingDates/Create

        public ActionResult Create()
        {
            ViewBag.PossibleFiles = context.Files;
            return View();
        } 

        //
        // POST: /TeachingDates/Create

        [HttpPost]
        public ActionResult Create(TeachingDate teachingdate)
        {
            if (ModelState.IsValid)
            {
                context.TeachingDates.Add(teachingdate);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleFiles = context.Files;
            return View(teachingdate);
        }
        
        //
        // GET: /TeachingDates/Edit/5
 
        public ActionResult Edit(int id)
        {
            TeachingDate teachingdate = context.TeachingDates.Single(x => x.Id == id);
            ViewBag.PossibleFiles = context.Files;
            return View(teachingdate);
        }

        //
        // POST: /TeachingDates/Edit/5

        [HttpPost]
        public ActionResult Edit(TeachingDate teachingdate)
        {
            if (ModelState.IsValid)
            {
                context.Entry(teachingdate).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleFiles = context.Files;
            return View(teachingdate);
        }

        //
        // GET: /TeachingDates/Delete/5
 
        public ActionResult Delete(int id)
        {
            TeachingDate teachingdate = context.TeachingDates.Single(x => x.Id == id);
            return View(teachingdate);
        }

        //
        // POST: /TeachingDates/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TeachingDate teachingdate = context.TeachingDates.Single(x => x.Id == id);
            context.TeachingDates.Remove(teachingdate);
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